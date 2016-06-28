using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using YoukaiHuntress.Components;

namespace YoukaiHuntress.Actors
{
    public abstract class Actor
    {
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public Vector2 origin { get; set; }
        public Point area { get; set; }
        public float nextMoveX { get; set; }
        public float nextMoveY { get; set; }
        public Rectangle fullCollision { get; set; }
        public Rectangle xCollision { get; set; }
        public Rectangle yCollision { get; set; }
        public Rectangle source { get; set; }
        public SpriteEffects effect { get; set; }
        public int spriteVariation { get; set; }
        public int spriteLength { get; set; }

        public Actor()
        {
            nextMoveX = nextMoveY = 0;
            effect = SpriteEffects.None;
        }

        public abstract void Draw(SpriteBatch sb, GameTime gameTime);
        public abstract void Update(GameTime gameTime, InputHandler input);

        public void Move() {
            position += new Vector2(nextMoveX, nextMoveY);
        }

        public void MoveX() {
            position += new Vector2(nextMoveX, 0);
        }

        public void MoveY() {
            position += new Vector2(0, nextMoveY);
        }
    }
}
