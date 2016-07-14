using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using YoukaiHuntress.Components;
using Microsoft.Xna.Framework.Audio;

namespace YoukaiHuntress.Actors.Player
{
    public class AttackState : State
    {
        public Sango sango { get; set; }
        public StateManager state { get; set; }
        float currentTime = 0f;
        Vector2 auxPosition;
        SoundEffect boomerangSnd;
        SoundEffectInstance boomerangSndInstance;

        public AttackState(Sango sango, StateManager state)
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
            boomerangSndInstance = boomerangSnd.CreateInstance();
            boomerangSndInstance.Play();
            sango.area = new Point(20, 42);
            sango.spriteVariation = 0;
            sango.spriteLength = 13;
            
        }

        public void Leave()
        {
            Console.WriteLine("Creating Boomerang");
            sango.boomerang = new Boomerang(sango.position, sango.texture, sango.effect);
            boomerangSndInstance.Stop();
        }

        public void LoadContent(ContentManager content)
        {
            sango.texture = content.Load<Texture2D>("Sango");
            boomerangSnd = content.Load<SoundEffect>("Boomerang");
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            auxPosition = sango.position + new Vector2(0, -15);
            SetCollisionReagion();
            Timer(gameTime);
            SetSource();
        }

        private void SetSource()
        {
            int auxVar = (62 * sango.spriteVariation);
            sango.source = new Rectangle(auxVar, 110, 62, 55);
        }

        private void SetCollisionReagion()
        {
            Vector2 auxPosition = sango.position + new Vector2(10, 0);
            sango.fullCollision = new Rectangle(auxPosition.ToPoint(), sango.area);
        }

        public void Timer(GameTime gameTime)
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
                state.ChangeState(new WaitState(sango, state));
            }
        }
    }
}
