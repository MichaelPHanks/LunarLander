using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLander
{
    public class HelpView : GameStateView
    {
        public override void loadContent(ContentManager contentManager)
        {
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            return GameStateEnum.Help;
        }

        public override void render(GameTime gameTime)
        {
            // Render some controls and how to play the game...
        }

        public override void update(GameTime gameTime)
        {
            // Nothing here...
        }
    }
}
