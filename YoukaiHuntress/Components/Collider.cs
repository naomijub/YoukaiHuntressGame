using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using YoukaiHuntress.Actors;
using YoukaiHuntress.Actors.Player;
using YoukaiHuntress.Level;
using YoukaiHuntress.LevelManager;

namespace YoukaiHuntress.Components
{
    public class Collider
    {
        public Collider() { }

        public void Collide(IList<Rectangle> collisionRectangles, Actor actors) {
            Sango sangoAux = (Sango)actors;

            if (!sangoAux.hasJumped)
            {
                regularCollision(collisionRectangles, actors);
            }
            else {
                jumpCollision(collisionRectangles, actors);
            }
            
        }

        private void jumpCollision(IList<Rectangle> collisionRectangles, Actor actors)
        {
            Sango sangoJump = (Sango)actors;
            bool xCollide = false;
            bool yCollide = false;

            foreach (Rectangle rect in collisionRectangles)
            {

                if (actors.xCollision.Intersects(rect))
                {
                    xCollide = true;
                    //Console.WriteLine("X : " + rect.ToString() + " Sango: " + sango.xCollision.ToString());

                }
                if (actors.yCollision.Intersects(rect))
                {
                    yCollide = true;
                    //Console.WriteLine("Y : "+rect.ToString() + " Sango: " + sango.xCollision.ToString());
                }
            }

            if (!xCollide && !yCollide)
            {
                actors.position += sangoJump.jumpVector;
            }
            else if (!xCollide && yCollide)
            {
                actors.position = new Vector2(actors.position.X + sangoJump.jumpVector.X, actors.position.Y );
            }
            else if (xCollide && !yCollide)
            {
                actors.position = new Vector2(actors.position.X, actors.position.Y + sangoJump.jumpVector.Y);
            }
        }

        private void regularCollision(IList<Rectangle> collisionRectangles, Actor actors)
        {
            bool xCollide = false;
            bool yCollide = false;

            foreach (Rectangle rect in collisionRectangles)
            {

                if (actors.xCollision.Intersects(rect))
                {
                    xCollide = true;
                    //Console.WriteLine("X : " + rect.ToString() + " Sango: " + sango.xCollision.ToString());

                }
                if (actors.yCollision.Intersects(rect))
                {
                    yCollide = true;
                    //Console.WriteLine("Y : "+rect.ToString() + " Sango: " + sango.xCollision.ToString());
                }
            }

            if (!xCollide && !yCollide)
            {
                actors.Move();
            }
            else if (!xCollide && yCollide)
            {
                actors.MoveX();
            }
            else if (xCollide && !yCollide)
            {
                actors.MoveY();
            }
        }

        public void Gravity(IList<Rectangle> collisionRectangles, Actor actors) {
            Point origin = actors.origin.ToPoint() + new Point(0, 1);
            Sango sango = (Sango)actors;
            
                bool yCollide = false;

                foreach (Rectangle rect in collisionRectangles)
                {
                    if (rect.Contains(origin))
                    {
                        yCollide = true;
                        //Console.WriteLine("Y : "+rect.ToString() + " Sango: " + sango.xCollision.ToString());
                    }
                }
                if (!yCollide)
                {
                    actors.position += new Vector2(0, 1);
                }
            
        }

        public void reactangles(IList<Rectangle> collisionRectangles) {
            foreach (Rectangle rect in collisionRectangles)
            {
                Console.WriteLine(rect.ToString());
            }
        }
    }
}
