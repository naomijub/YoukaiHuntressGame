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
    public class BatState : State
    {
        public StateManager state { get; set; }
        public Actor youkai { get; set; }
        float currentTime = 0f;
        Random rg;
        float yDir = -2.0f;

        public BatState(Actor youkai, StateManager state) {
            this.youkai = youkai;
            this.state = state;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(youkai.texture, youkai.position, youkai.source, Color.White);
        }

        public void Enter()
        {
            youkai.position = new Vector2(1600, 440);
            youkai.spriteVariation = 0;
            youkai.spriteLength = 2;
            youkai.area = new Point(32, 24);
            rg = new Random();
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
            move();
            setCollisionRegion();
            setSource();
        }

        private void move()
        {
            if (youkai.position.X < 0)
            {
                youkai.position = new Vector2(1920, rg.Next(340, 470));
            }
            else {
                if (youkai.position.Y < 350)
                {
                    yDir = -yDir;
                }
                else if (youkai.position.Y > 480) {
                    yDir = -yDir;
                }
                float xDir = youkai.position.X - 2.0f; 
                youkai.position = new Vector2(xDir, youkai.position.Y + yDir);
            }
        }

        private void setSource()
        {
            int auxVar = (32 * youkai.spriteVariation);
            youkai.source = new Rectangle(auxVar, 116, youkai.area.X, youkai.area.Y);
        }

        private void setCollisionRegion()
        {
            youkai.fullCollision = new Rectangle((int)youkai.position.X + 10, (int)youkai.position.Y + 5, 10, 10);
        }

        public void Timer(GameTime gameTime)
        {
            float duration = 0.08f;

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
