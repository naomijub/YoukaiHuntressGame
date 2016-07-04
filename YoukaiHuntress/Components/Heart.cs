using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace YoukaiHuntress.Components
{
    public class Heart
    {
        public Texture2D heart { get; set; }
        public Vector2 position { get; set; }
        public Rectangle collision { get; set; }
        public bool isAvailable { get; set; }

        public Heart(Vector2 position, Texture2D heart) {
            this.position = position;
            this.heart = heart;
            isAvailable = true;
            Vector2 aux = position + new Vector2(3, 2);
            collision = new Rectangle(aux.ToPoint(), new Point(10, 10));
        }

        

        public void Draw(SpriteBatch sb) {
            if(isAvailable)
            sb.Draw(heart, position, Color.White);
        }

        public void Update(Rectangle sango) {
            if (sango.Intersects(collision)) {
                isAvailable = false;
            }
        }
    }
}
