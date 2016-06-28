using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace YoukaiHuntress.Components
{
    public interface State
    {
        void LoadContent(ContentManager content);
        void Enter();
        void Leave();
        void Draw(SpriteBatch sb, GameTime gameTime);
        void Update(GameTime gameTime, InputHandler inputHandler);
    }
}
