using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using YoukaiHuntress.Components;

namespace YoukaiHuntress.Actors.Player
{
    public class WaitState : State
    {
        public Sango sango { get; set; }
        public StateManager state { get; set; }
        public float distance { get; set; }

        public WaitState(Sango sango, StateManager state)
        {
            this.sango = sango;
            this.state = state;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(sango.texture, sango.position, null, sango.source, null, 0.0f, null, Color.White, sango.effect, 0.0f);
            sango.boomerang.Draw(sb, gameTime);
        }

        public void Enter()
        {
            sango.area = new Point(24, 40);
        }

        public void Leave()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            sango.texture = content.Load<Texture2D>("Sango");
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            sango.boomerang.Update(gameTime, inputHandler);
            distance += 0.1f;
            SetSource();
            SetCollisionReagion();
            checkDistance();
        }

        private void checkDistance()
        {
            if (sango.boomerang.initBool) {
                sango.boomerang = null;
                state.ChangeState(new GrabState(sango, state));
            }
        }

        private void SetSource()
        {
            
            sango.source = new Rectangle(6, 231, 32, 44);
        }

        private void SetCollisionReagion()
        {
            Vector2 auxPosition = sango.position;
            sango.fullCollision = new Rectangle(auxPosition.ToPoint(), sango.area);
        }
    }
}
