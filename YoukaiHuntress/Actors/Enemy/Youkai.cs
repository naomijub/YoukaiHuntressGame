using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YoukaiHuntress.Components;

namespace YoukaiHuntress.Actors.Enemy
{
    public class Youkai : Actor
    {
        public StateManager state { get; set; }

        public Youkai(StateManager state) {
            this.state = state;
        }

        public void setState(State primaryState) {
            this.state.setPrimaryState(primaryState);
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
