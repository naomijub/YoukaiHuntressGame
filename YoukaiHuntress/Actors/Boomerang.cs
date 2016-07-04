using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace YoukaiHuntress.Actors
{
    public class Boomerang : Actor
    {
        public Vector2 initialPos { get; set; }
        float currentTime = 0f;
        public bool initBool { get; set; }
        bool away = false;
        int time = 0;
        SpriteEffects effect;

        public Boomerang(Vector2 sangoPos, Texture2D texture, SpriteEffects effect) {
            this.texture = texture;
            this.effect = effect;
            initialPos = sangoPos + new Vector2(26, 0);
            this.position = initialPos;
            spriteVariation = 0;
            spriteLength = 3;
            initBool = false;
            Console.WriteLine("Created Boomerang");
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            Console.WriteLine("Draw Boomerang");
            sb.Draw(texture, position, source, Color.White);
        }

        public override void Update(GameTime gameTime, InputHandler input)
        {
            checkPos();
            setSource();
            setCollison();
            Timer(gameTime);
        }

        private void setCollison()
        {
            Vector2 auxPos = position + new Vector2(6,8);
            fullCollision = new Rectangle(auxPos.ToPoint(), new Point(24, 12));
        }

        private void setSource()
        {
            int auxVar = (45 * spriteVariation);
            source = new Rectangle(auxVar, 287, 45, 22);
        }

        private void checkPos()
        {
            if ((!away && ((position.X > initialPos.X - 2) || (position.X < initialPos.X + 2))) ||
                (away && ((position.X < initialPos.X - 2) || (position.X > initialPos.X + 2))))
            {
                double xPos = 8 - Math.Sqrt(time);
                if (effect == SpriteEffects.FlipHorizontally) xPos = -xPos;
                position += new Vector2((float)xPos, 0);
                Console.WriteLine("Boomerang pos = " + xPos);
                //initBool = true;
                time++;
            }
            else {
                initBool = true;
            }
        }

        public void Timer(GameTime gameTime)
        {
            float duration = 0.1f;

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime >= duration)
            {
                spriteVariation++;
                currentTime -= duration;
            }
            if (spriteVariation >= spriteLength)
            {
                spriteVariation = 0;
                away = true;
            }
        }
    }
}
