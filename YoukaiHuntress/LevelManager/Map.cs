using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoukaiHuntress.LevelManager
{
    public class Map
    {
        public Manager manager { get; set; }
        public Layers currentLevel { get; set; }
        public ContentManager content { get; set; }
        public Texture2D texture { get; set; }
        static Point area;
        Rectangle source;
        Vector2 position;
        public IList<Rectangle> collisionRectangles { get; set; }

        public Map(Manager manager, ContentManager content)
        {
            this.manager = manager;
            this.content = content;
            texture = content.Load<Texture2D>("Tiles_32x32");
            area = new Point(32, 32);
        }

        public void ChangeLevel(string name)
        {
            for (int i = 0; i < manager.levels.Count; i++)
            {
                for (int j = 0; j < manager.levels[i].layers.Count; j++)
                {
                    //Console.WriteLine(manager.levels[i].layers[j].name);
                    //Console.WriteLine(name == manager.levels[i].layers[j].name);
                    if (name == manager.levels[i].layers[j].name)
                    {
                        currentLevel = manager.levels[i].layers[j];
                    }
                }
            }
        }
        public void Update(Vector2 playerOrigin) {
            Point XY0 = XY(playerOrigin.ToPoint());
            collisionRectangles = new List<Rectangle>();

            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x <  60; x++)
                {
                    int i = x + y * 60;
                    //Console.Write(currentLevel.data[i]+" ");
                    if (currentLevel.data[i] != 0 && currentLevel.data[i] != 29)
                    {
                        //Console.Write(x+" ");
                        position = new Vector2(32 * x, (y * 32));
                        collisionRectangles.Add(new Rectangle(position.ToPoint(), new Point(32, 32)));
                        
                    }
                }
                //Console.WriteLine();
            }
            
        }

        public void Draw(SpriteBatch sb, Vector2 playerOrigin)
        {
            Point XY0 = XY(playerOrigin.ToPoint());
            //Console.WriteLine(currentLevel.data.Length);
            

            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 30; x++) {
                    int i = x + y * 60;
                    //Console.Write(currentLevel.data[i]+" ");
                    if (currentLevel.data[i] != 0)
                    {
                        Point pos = new Point(((currentLevel.data[i] - 1) % 8) * 32, ((int)((currentLevel.data[i] - 1) / 8)) * 32);
                        source = new Rectangle(pos, area);
                        position = new Vector2(32 * x, (y) * 32);
                        sb.Draw(texture, position, source, Color.White);
                    }
                }
                //Console.WriteLine();
            }
            //Console.WriteLine();
            
        }

        public Point XY(Point XY0) {
            int x = 0, y = 0;
            if (XY0.X <= 3)
            {
                x = 0;
            }
            else if (XY0.X > 30)
            {
                x = 30;
            }
            else {
                x = XY0.X;
            }

            return new Point(x, y);
        }
    }
}
