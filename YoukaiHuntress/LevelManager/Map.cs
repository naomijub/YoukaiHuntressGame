using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoukaiHuntress.Components;

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
        public IList<Heart> hearts { get; set; }

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
        public void Update() {
            collisionRectangles = new List<Rectangle>();

            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x <  60; x++)
                {
                    int i = x + y * 60;
                    //Console.Write(currentLevel.data[i]+" ");
                    if (currentLevel.data[i] != 0 && currentLevel.data[i] != 29 && currentLevel.data[i] != 25 && currentLevel.data[i] != 26 &&
                        currentLevel.data[i] != 53 && currentLevel.data[i] != 54)
                    {
                        //Console.Write(x+" ");
                        position = new Vector2(32 * x - 1, (y * 32));
                        collisionRectangles.Add(new Rectangle(position.ToPoint(), new Point(34, 32)));
                        
                    }
                    if (currentLevel.data[i] == 25 || currentLevel.data[i] == 26 ||
                        currentLevel.data[i] == 53 | currentLevel.data[i] == 54) {
                     
                        slopeRectangles(currentLevel.data[i], x, y);
                        
                    }
                }
                //Console.WriteLine();
            }
            
        }

        private void slopeRectangles(int code, int x, int y)
        {
            switch (code) {
                case 25: collisionRectangles.Add(new Rectangle(new Point(x * 32, y * 32 + 2), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 4, y * 32 + 4), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 8, y * 32 + 6), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 12, y * 32 + 8), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 16, y * 32 + 10), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 20, y * 32 + 12), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 24, y * 32 + 14), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 28, y * 32 + 16), new Point(4, 8)));
                    break;
                case 26:
                    collisionRectangles.Add(new Rectangle(new Point(x * 32, y * 32 + 2 + 16), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 4, y * 32 + 4 + 16), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 8, y * 32 + 6 + 16), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 12, y * 32 + 8 + 16), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 16, y * 32 + 10 + 16), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 20, y * 32 + 12 + 16), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 24, y * 32 + 14 + 16), new Point(4, 8)));
                    collisionRectangles.Add(new Rectangle(new Point(x * 32 + 28, y * 32 + 16 + 16), new Point(4, 8)));
                    break;
            }
        }

        public void Draw(SpriteBatch sb)
        {

            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 60; x++) {
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
            foreach (Heart hrt in hearts) {
                hrt.Draw(sb);
            }
            
        }

        private void SetHearts(ContentManager content)
        {
            hearts = new List<Heart>();
            hearts.Add(new Heart(new Vector2(264, 448), content.Load<Texture2D>("heart")));
            hearts.Add(new Heart(new Vector2(616, 330), content.Load<Texture2D>("heart")));
            hearts.Add(new Heart(new Vector2(1896, 426), content.Load<Texture2D>("heart")));
        }
    }
}
