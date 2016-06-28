using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoukaiHuntress.LevelManager
{
    public class Level
    {
        public int height { get; set; }
        public IList<Layers> layers { get; set; }
        public int nextobjectid { get; set; }
        public string orientation { get; set; }
        public string renderorder { get; set; }
        public int tileheight { get; set; }
        public IList<Tilesets> tilesets { get; set; }
        public int tilewidth { get; set; }
        public string version { get; set; }
        public int width { get; set; }

        public Level() { }
    }
}
