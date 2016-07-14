using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoukaiHuntress.Components;
using YoukaiHuntress.Actors.Objects;

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
        public IList<Rectangle> spikesRectangles { get; set; }
        //public IList<Triangle> triangles { get; set; }
        public IList<Heart> hearts { get; set; }
        public IList<Actors.Objects.Object> objects { get; set; }

        public Map(Manager manager, ContentManager content)
        {
            texture = content.Load<Texture2D>("Tiles_32x32");
            collisionRectangles = new List<Rectangle>();
            spikesRectangles = new List<Rectangle>();
            //triangles = new List<Triangle>();
            objects = new List<Actors.Objects.Object>();
            objects.Add(new Actors.Objects.Object(2, new Vector2(160, 416), texture, 21));
            objects.Add(new Actors.Objects.Object(2, new Vector2(256, 384), texture, 21));
            objects.Add(new Actors.Objects.Object(2, new Vector2(366, 352),  texture, 21));
            objects.Add(new Actors.Objects.Object(2, new Vector2(462, 384), texture, 21));
            objects.Add(new Actors.Objects.Object(2, new Vector2(1420, 448), texture, 21));
            objects.Add(new Actors.Objects.Object(1, new Vector2(1504, 336), texture, 21));
            objects.Add(new Actors.Objects.Object(1, new Vector2(800, 512), texture, 1));
            objects.Add(new Actors.Objects.Object(1, new Vector2(832, 528), texture, 1));
            objects.Add(new Actors.Objects.Object(1, new Vector2(864, 544), texture, 1));
            objects.Add(new Actors.Objects.Object(1, new Vector2(896, 560), texture, 1));
            objects.Add(new Actors.Objects.Object(1, new Vector2(992, 560), texture, 1));
            objects.Add(new Actors.Objects.Object(1, new Vector2(1024, 544), texture, 1));
            objects.Add(new Actors.Objects.Object(1, new Vector2(1056, 528), texture, 1));
            objects.Add(new Actors.Objects.Object(1, new Vector2(1088, 512), texture, 1));
            this.manager = manager;
            this.content = content;
            area = new Point(32, 32);

            setCollisonObjects();
        }

        public void setCollisonObjects() {
            foreach (Actors.Objects.Object obj in objects)
            {
                collisionRectangles.Add(obj.fullCollision);
            }
        }

        public void setRectangles() {
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 60; x++)
                {
                    int i = x + y * 60;
                    //Console.Write(currentLevel.data[i]+" ");
                    if (currentLevel.data[i] != 0 && currentLevel.data[i] != 29 && currentLevel.data[i] != 25 && currentLevel.data[i] != 26 &&
                        currentLevel.data[i] != 53 && currentLevel.data[i] != 54 && currentLevel.data[i] != 12 && currentLevel.data[i] != 14 &&
                        currentLevel.data[i] != 35 && currentLevel.data[i] != 36 && currentLevel.data[i] != 37 && currentLevel.data[i] != 47)
                    {
                        //Console.Write(x+" ");
                        position = new Vector2(32 * x - 1, (y * 32));
                        collisionRectangles.Add(new Rectangle(position.ToPoint(), new Point(34, 32)));

                    }
                    //if (currentLevel.data[i] == 25 || currentLevel.data[i] == 26 ||
                    //    currentLevel.data[i] == 53 || currentLevel.data[i] == 54)
                    //{
                    //    //Console.WriteLine("Code = " + currentLevel.data[i] + " X / Y: " + x + "/" + y);
                    //    Vector2[] trian = new Vector2[3];
                    //    trian = setTriangles(currentLevel.data[i], x, y);
                    //    triangles.Add(new Triangle(trian));
                    //}
                    if (currentLevel.data[i] == 47) {
                        spikesRectangles.Add(new Rectangle(x * 32, y * 32 + 20, 32, 12));
                    }
                }
                //Console.WriteLine();
            }
        }

        private Vector2[] setTriangles(int code, int x, int y)
        {
            Vector2[] aux = new Vector2[3];
            switch (code) {
                case 25: aux[0] = new Vector2(x * 32, y * 32); aux[1] = new Vector2((x + 1) * 32, 16 + y * 32); aux[2] = new Vector2(x * 32, 16 + y * 32); break;
                case 26: aux[0] = new Vector2(x * 32, y * 32 + 16); aux[1] = new Vector2((x + 1) * 32, (1 + y) * 32); aux[2] = new Vector2(x * 32, ( 1 + y) * 32); break;
                case 53: aux[0] = new Vector2(x * 32, ( 1 + y) * 32); aux[1] = new Vector2((x + 1) * 32, y * 32  + 16); aux[2] = new Vector2((x + 1) * 32, y * 32 + 32); break;
                case 54: aux[0] = new Vector2(x * 32, y * 32 + 16); aux[1] = new Vector2((x + 1) * 32, y * 32 ); aux[2] = new Vector2((x + 1) * 32, y * 32 + 16); break;
            }
            return aux;
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

            setRectangles();
        }
        public void Update(InputHandler input, GameTime time) {

        }
        

        public void Draw(SpriteBatch sb, GameTime time)
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
            foreach (Actors.Objects.Object obj in objects) {
                obj.Draw(sb, time);
            }
            
        }

        public void SetHearts(ContentManager content)
        {
            hearts = new List<Heart>();
            hearts.Add(new Heart(new Vector2(264, 448), content.Load<Texture2D>("heart")));
            hearts.Add(new Heart(new Vector2(616, 330), content.Load<Texture2D>("heart")));
            hearts.Add(new Heart(new Vector2(1896, 426), content.Load<Texture2D>("heart")));
        }
    }
}
