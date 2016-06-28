using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace YoukaiHuntress.Actors.Player
{
    public class Sango : Actor
    {
        public StateManager state { get; set; }
        public Vector2 jumpVector { get; set; }
        public bool hasJumped { get; set; }

        public Sango(StateManager state) : base() {
            this.state = state;
            this.state.setPrimaryState(new WalkIdleState(this, this.state));
            position = new Vector2(36,534);
            origin = position + new Vector2(15, 21);
            hasJumped = false;
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            state.Draw(sb, gameTime);
        }

        public override void Update(GameTime gameTime, InputHandler input)
        {
            state.Update(gameTime);
        }
    }
}
