using LunarLander.InputHandling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System;

namespace LunarLander
{
    public class LunarLander : Game
    {
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;
        private IGameState m_currentState;
        private IGameState m_prevState;
        private Dictionary<GameStateEnum, IGameState> m_gameStates;
        private IGameState savedGamePlay;
        
        private SettingsView m_settings;
        private GamePlayView m_gamePlayView;
        private GameStateEnum m_gameState;
        private HelpView m_helpView;


        public LunarLander()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()    
        {
            // This sets up the highscores and key bindings, if they do not exist.
            setUpFiles();


            //m_graphics.IsFullScreen = true;
            m_graphics.PreferredBackBufferWidth = 1920;
            m_graphics.PreferredBackBufferHeight = 1080;
            m_graphics.ApplyChanges();


            // TODO: Add your initialization logic here
            m_settings = new SettingsView();
            m_gamePlayView = new GamePlayView();
            m_helpView = new HelpView();
            m_gameStates = new Dictionary<GameStateEnum, IGameState>();
            m_gameStates.Add(GameStateEnum.About, new AboutView());
            m_gameStates.Add(GameStateEnum.MainMenu, new MainMenuView());
            m_gameStates.Add(GameStateEnum.GamePlay, m_gamePlayView);
            m_gameStates.Add(GameStateEnum.Paused, new PauseView());
            m_gameStates.Add(GameStateEnum.Settings, m_settings);
            m_gameStates.Add(GameStateEnum.HighScores, new HighScoresView());
            m_gameStates.Add(GameStateEnum.Help, m_helpView);

            foreach (var item in m_gameStates)
            {
                item.Value.initialize(this.GraphicsDevice, m_graphics);
            }

            m_currentState = m_gameStates[GameStateEnum.MainMenu];
            m_prevState = m_gameStates[GameStateEnum.MainMenu];
            m_gameState = GameStateEnum.MainMenu;
            
            
            base.Initialize();
        }


        private void setUpFiles()
        {
            lock (this)
            {
                initializeFiles();
            }
        }


        /// <summary>
        ///     If this is the first time running on this computer 
        ///     (if the key-bindings and highscores do not exist), 
        ///     create the files
        /// </summary>
        private async Task initializeFiles()
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {

                    try
                    {
                        if (!storage.FileExists("KeyControls.json"))
                        {
                            saveDefualtControls(new KeyControls(Keys.Left, Keys.Right, Keys.Up));
                        }
                        
                    }
                    catch (IsolatedStorageException)
                    {


                    }

                    try 
                    {
                        if (!storage.FileExists("HighScores.json"))
                        {
                            saveDefualtHighScores(new HighScoresState(new List<Tuple<int, DateTime>> { }));
                        }
                    }
                    catch (IsolatedStorageException) { }
                }

            });
        }

        private void saveDefualtHighScores(HighScoresState highScore)
        {
            lock (this)
            {
                finalizeSaveAsyncHighScores(highScore);
            }
        }

        private async Task finalizeSaveAsyncHighScores(HighScoresState state)
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("HighScores.json", FileMode.Create))
                        {
                            if (fs != null)
                            {
                                DataContractJsonSerializer mySerializer = new DataContractJsonSerializer(typeof(HighScoresState));
                                mySerializer.WriteObject(fs, state);
                            }
                        }
                    }
                    catch (IsolatedStorageException)
                    {
                    }
                }

            });
        }
        /// <summary>
        /// Demonstrates how serialize an object to storage
        /// </summary>
        private void saveDefualtControls(KeyControls controls)
        {
            lock (this)
            {
                    finalizeSaveAsyncKeyBindings(controls);
            }
        }


        private async Task finalizeSaveAsyncKeyBindings(KeyControls state)
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("KeyControls.json", FileMode.Create))
                        {
                            if (fs != null)
                            {
                                DataContractJsonSerializer mySerializer = new DataContractJsonSerializer(typeof(KeyControls));
                                mySerializer.WriteObject(fs, state);
                            }
                        }
                    }
                    catch (IsolatedStorageException)
                    {
                        // Ideally show something to the user, but this is demo code :)
                    }
                }

            });
        }

        protected override void LoadContent()
        {

            foreach (var item in m_gameStates)
            {
                item.Value.loadContent(this.Content);
            }
            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            GameStateEnum nextStateEnum = m_currentState.processInput(gameTime);
            
            if (nextStateEnum == GameStateEnum.Exit)
            {
                Exit();
            }

            else
            {
                m_currentState.update(gameTime);

                
                if (m_prevState == m_gameStates[GameStateEnum.MainMenu] && nextStateEnum == GameStateEnum.GamePlay)
                {
                    m_gamePlayView.resetGameplay();
                    
                }

                if (m_prevState == m_gameStates[GameStateEnum.GamePlay] && nextStateEnum == GameStateEnum.Paused)
                {
                    savedGamePlay = m_currentState;
                }



                

                if (nextStateEnum == GameStateEnum.Settings && m_gameState != GameStateEnum.Settings)
                {
                    
                        m_settings.prevState =m_gameState;

                    
                }
                

                if (nextStateEnum == GameStateEnum.Help && m_gameState != GameStateEnum.Help)
                {

                    m_helpView.helpPrevState = m_gameState;


                }

                if (nextStateEnum == GameStateEnum.HighScores)
                {
                    m_gameStates[nextStateEnum] = null;
                    m_gameStates[nextStateEnum] = new HighScoresView();
                    m_gameStates[nextStateEnum].initialize(this.GraphicsDevice, m_graphics);
                    m_gameStates[nextStateEnum].loadContent(this.Content);


                }


                m_currentState = m_gameStates[nextStateEnum];
                m_prevState = m_gameStates[nextStateEnum];
                m_gameState = nextStateEnum;

            }

            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            m_currentState.render(gameTime);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}