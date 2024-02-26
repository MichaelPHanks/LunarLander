using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLander
{
    public class SettingsView : GameStateView
    {



        
        

        
        public override void loadContent(ContentManager contentManager)
        {
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            return GameStateEnum.Settings;
        }

        public override void render(GameTime gameTime)
        {
        }

        public override void update(GameTime gameTime)
        {
        }
    }
}
