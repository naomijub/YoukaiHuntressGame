using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace YoukaiHuntress.Actors.Objects
{
    public class Object : Actor
    {
        
        public Point area { get; set; }

        public Object(int blocks, Vector2 pos, Texture2D texture, int iTexture) {
            if (iTexture == 1) {
                area = new Point(33, 32);
             }
            else {
                area = new Point(32 * blocks, 32);
            }
            this.texture = texture;
            position = pos;
            if (position.X > 1800)
            {

                source = new Rectangle(0,0, 63, 43);
            }
            else {

                source = new Rectangle((iTexture % 8) * 32, (int)(iTexture / 8) * 32, 32 * blocks, 32);
            }
            setCollisonRegion();
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(texture, position, source, Color.White);
        }

        public override void Update(GameTime gameTime, InputHandler input)
        {
            
        }

        private void setCollisonRegion()
        {
            fullCollision = new Rectangle(position.ToPoint(), area);
        }
    }
}
