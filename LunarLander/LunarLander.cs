using LunarLander.InputHandling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
        private Keys up;
        private Keys right;
        private Keys left;
        private SettingsView m_settings;
        private GamePlayView m_gamePlayView;
        private GameStateEnum m_gameState;


        public LunarLander()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()    
        {
            m_graphics.IsFullScreen = true;
            m_graphics.PreferredBackBufferWidth = 1920;
            m_graphics.PreferredBackBufferHeight = 1080;
            m_graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            m_settings = new SettingsView();
            m_gamePlayView = new GamePlayView();
            m_gameStates = new Dictionary<GameStateEnum, IGameState>();
            m_gameStates.Add(GameStateEnum.About, new AboutView());
            m_gameStates.Add(GameStateEnum.MainMenu, new MainMenuView());
            m_gameStates.Add(GameStateEnum.GamePlay, m_gamePlayView);
            m_gameStates.Add(GameStateEnum.Paused, new PauseView());
            m_gameStates.Add(GameStateEnum.Settings, m_settings);

            foreach (var item in m_gameStates)
            {
                item.Value.initialize(this.GraphicsDevice, m_graphics);
            }

            m_currentState = m_gameStates[GameStateEnum.MainMenu];
            m_prevState = m_gameStates[GameStateEnum.MainMenu];
            m_gameState = GameStateEnum.MainMenu;
            up = Keys.W;
            right = Keys.D;
            left = Keys.A;
            
            base.Initialize();
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

                // TODO: Need to make it so it saves the previous game state, and can resume when paused.
                // Also so we can start a new game once we go to the main menu.

                // If the previous game state we have is Main menu and the next is gameplay, make a new game

                // If the previous game state is gameplay and the next is pausing, save the gameplay.

                // If the previous game state is pause view, and the next is the main menu,
                // keep the saved gameplay (should already be save anyways).

                // If the saved gameplay is not null, the next game state is gameplay, and the previous game state is paused, load that gameplay
                
                if (m_prevState == m_gameStates[GameStateEnum.MainMenu] && nextStateEnum == GameStateEnum.GamePlay)
                {
                    m_gamePlayView.resetGameplay();
                    
                }

                if (m_prevState == m_gameStates[GameStateEnum.GamePlay] && nextStateEnum == GameStateEnum.Paused)
                {
                    savedGamePlay = m_currentState;
                }


                // If the previous game state is settings, we need to check for differences in the keys used for gameplay.

                if (m_prevState == m_gameStates[GameStateEnum.Settings])
                {
                    if (m_settings.up != up)
                    {
                        m_gamePlayView.ModifyKey(KeyEnum.Up,m_settings.up);
                        up = m_settings.up;
                    }
                    if (m_settings.left != left)
                    {
                        m_gamePlayView.ModifyKey(KeyEnum.Left, m_settings.left);
                        left = m_settings.left;
                    }
                    if (m_settings.right != right)
                    {
                        m_gamePlayView.ModifyKey(KeyEnum.Right, m_settings.right);
                        right = m_settings.right;
                    }

                }

                if (nextStateEnum == GameStateEnum.Settings && m_gameState != GameStateEnum.Settings)
                {
                    
                        m_settings.prevState =m_gameState;

                    
                }
                
               /* if (m_prevState == m_gameStates[GameStateEnum.Paused] && nextStateEnum == GameStateEnum.GamePlay && savedGamePlay != null)
                {
                    m_currentState = savedGamePlay;
                }*/

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