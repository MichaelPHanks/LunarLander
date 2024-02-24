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
        

        public LunarLander()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            m_gameStates = new Dictionary<GameStateEnum, IGameState>();
            m_gameStates.Add(GameStateEnum.About, new AboutView());
            m_gameStates.Add(GameStateEnum.MainMenu, new MainMenuView());
            m_gameStates.Add(GameStateEnum.GamePlay, new GamePlayView());
            m_gameStates.Add(GameStateEnum.Paused, new PauseView());
            //m_gameStates.Add(GameStateEnum.Settings, new SettingsView());

            foreach (var item in m_gameStates)
            {
                item.Value.initialize(this.GraphicsDevice, m_graphics);
            }

            m_currentState = m_gameStates[GameStateEnum.MainMenu];
            m_prevState = m_gameStates[GameStateEnum.MainMenu];
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
                if (m_prevState != m_gameStates[nextStateEnum] && nextStateEnum == GameStateEnum.GamePlay)
                {
                    m_gameStates[nextStateEnum] = new GamePlayView();
                    m_gameStates[nextStateEnum].initialize(this.GraphicsDevice, m_graphics);
                    m_gameStates[nextStateEnum].loadContent(this.Content);
                }
                m_currentState = m_gameStates[nextStateEnum];
                m_prevState = m_gameStates[nextStateEnum];
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