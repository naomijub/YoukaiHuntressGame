using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoukaiHuntress.LevelManager
{
    public class Tilesets
    {
        public int columns { get; set; }
        public int firstgid { get; set; }
        public int imageheight { get; set; }
        public int imagewidth { get; set; }
        public int margin { get; set; }
        public string name { get; set; }
        public int spacing { get; set; }
        public int tilecount { get; set; }
        public int tileheight { get; set; }
        public int tilewidth { get; set; }

        public Tilesets() { }
    }
}
