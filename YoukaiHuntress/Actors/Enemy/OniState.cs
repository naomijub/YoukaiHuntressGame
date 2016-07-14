using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using YoukaiHuntress.Components;

namespace YoukaiHuntress.Actors.Enemy
{
    public class OniState : State
    {
        public StateManager state { get; set; }
        public Actor youkai { get; set; }
        float currentTime = 0f;

        public OniState(Actor youkai, StateManager state, Vector2 position)
        {
            youkai.position = position;
            this.youkai = youkai;
            this.state = state;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(youkai.texture, youkai.position, youkai.source, Color.White);
        }

        public void Enter()
        {
            youkai.spriteVariation = 0;
            youkai.spriteLength = 3;
            youkai.area = new Point(28, 34);
            youkai.fullCollision = new Rectangle((int)youkai.position.X + 2, (int)youkai.position.Y + 4, 23, 30);
        }

        public void Leave()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            youkai.texture = content.Load<Texture2D>("Youkais");
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            Timer(gameTime);
            setSource();
        }

        private void setSource()
        {
            int auxVar = (28 * youkai.spriteVariation);
            youkai.source = new Rectangle(auxVar, 0, youkai.area.X, youkai.area.Y);
        }

        public void Timer(GameTime gameTime)
        {
            float duration = 0.09f;

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime >= duration)
            {
                youkai.spriteVariation++;
                currentTime -= duration;
            }
            if (youkai.spriteVariation >= youkai.spriteLength)
            {
                youkai.spriteVariation = 0;
            }
        }
    }
}
