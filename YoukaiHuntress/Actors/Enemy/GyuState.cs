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
    public class GyuState : State
    {
        public enum Behavior { walk, incline, roll}
        public StateManager state { get; set; }
        public Actor youkai { get; set; }
        float currentTime = 0f;
        Behavior behave = Behavior.walk;
        public SpriteEffects effect { get; set; }
        int initialPosX;

        public GyuState(Actor youkai, StateManager state, Vector2 position)
        {
            youkai.position = position;
            this.youkai = youkai;
            this.state = state;
            initialPosX = (int)position.X;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(youkai.texture, youkai.position, null, youkai.source, null, 0.0f, null, Color.White, effect, 0.0f);
        }

        public void Enter()
        {
            youkai.spriteVariation = 0;
            youkai.spriteLength = 3;
            effect = SpriteEffects.None;
            youkai.area = new Point(28, 40);
            youkai.fullCollision = new Rectangle(youkai.position.ToPoint(), youkai.area);
            youkai.nextMoveX = -2;
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
            move();
            checkDirection();
            checkRoll();
            Timer(gameTime);
            setSource();
            setCollision();
        }

        private void setCollision()
        {
            youkai.fullCollision = new Rectangle((int)youkai.position.X + 8, 11 + (int)youkai.position.Y, 16, 28);
        }

        private void checkRoll()
        {
            if (behave == Behavior.incline && youkai.spriteVariation >= 3) {
                behave = Behavior.roll;
                youkai.spriteVariation = 0;
                youkai.spriteLength = 2;
                youkai.nextMoveX = -3;
            }
        }

        public void checkDist(Actor sango)
        {
            double dist = Math.Sqrt(Math.Pow((sango.position.X - youkai.position.X), 2) + Math.Pow((sango.position.Y - youkai.position.Y), 2));
            if (dist < 150 && (behave != Behavior.incline && behave != Behavior.roll)) {
                behave = Behavior.incline;
                youkai.spriteVariation = 0;
                youkai.spriteLength = 5;
                youkai.area = new Point(32, 41);

            }
            if (dist > 250)
            {
                behave = Behavior.walk;
                youkai.spriteVariation = 0;
                youkai.spriteLength = 3;
            }
        }

        private void checkDirection()
        {
            if (behave == Behavior.walk)
            {
                if (youkai.position.X <= (initialPosX - 150))
                {
                    youkai.nextMoveX = +2;
                    effect = SpriteEffects.FlipHorizontally;
                }
                if (youkai.position.X >= initialPosX)
                {
                    youkai.nextMoveX = -2;
                    effect = SpriteEffects.None;
                }
            }
            else if (behave == Behavior.roll)
            {
                if (youkai.position.X <= (initialPosX - 150))
                {
                    youkai.nextMoveX = +3;
                    effect = SpriteEffects.FlipHorizontally;
                }
                if (youkai.position.X >= initialPosX + 25)
                {
                    youkai.nextMoveX = -3;
                    effect = SpriteEffects.None;
                }
            }
            else {
                youkai.nextMoveX = 0;
            }
        }

        private void move()
        {
            youkai.position = new Vector2(youkai.position.X + youkai.nextMoveX, youkai.position.Y);
        }

        private void setSource()
        {
            switch (behave) {
                case Behavior.walk:
                    int auxVar = (28 * youkai.spriteVariation);
                    youkai.source = new Rectangle(auxVar, 74, youkai.area.X, youkai.area.Y);
                    break;
                case Behavior.incline:
                     int auxVar2 = 84 + (32 * youkai.spriteVariation);
                    youkai.source = new Rectangle(auxVar2, 74, youkai.area.X, youkai.area.Y);
                    break;
                case Behavior.roll:
                    int auxVar3 = 180 + (32 * youkai.spriteVariation);
                    youkai.source = new Rectangle(auxVar3, 74, 32, youkai.area.Y);
                    break;

            }
        }

        public void Timer(GameTime gameTime)
        {
            float duration;
            if (behave == Behavior.roll)
            {
                duration = 0.1f;
            }
            else {
                duration = 0.13f;
            }

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
