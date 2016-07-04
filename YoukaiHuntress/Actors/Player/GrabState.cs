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
    public class GrabState : State
    {
        public Sango sango { get; set; }
        public StateManager state { get; set; }
        float currentTime = 0f;
        Vector2 auxPosition;

        public GrabState(Sango sango, StateManager state)
        {
            this.sango = sango;
            this.state = state;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(sango.texture, auxPosition, null, sango.source, null, 0.0f, null, Color.White, sango.effect, 0.0f);
        }

        public void Enter()
        {
            sango.area = new Point(20, 42);
            sango.spriteVariation = 0;
            sango.spriteLength = 13;
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
            auxPosition = sango.position + new Vector2(0, -15);
            SetCollisionReagion();
            Timer(gameTime);
            SetSource();
        }

        private void SetCollisionReagion()
        {
            sango.fullCollision = new Rectangle(sango.position.ToPoint(), sango.area);
        }

        private void SetSource()
        {
            int auxVar = 6 + (62 * sango.spriteVariation);
            sango.source = new Rectangle(auxVar, 165, 62, 55);
        }

        private void Timer(GameTime gameTime)
        {
            float duration = 0.1f;

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime >= duration)
            {
                sango.spriteVariation++;
                currentTime -= duration;
            }
            if (sango.spriteVariation >= sango.spriteLength)
            {
                state.ChangeState(new WalkIdleState(sango, state));
            }
        }
    }
}
