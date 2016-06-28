using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace YoukaiHuntress.LevelManager
{
    public class Manager
    {
        public IList<Level> levels { get; set; }

        public Manager()
        {
            string domain = AppDomain.CurrentDomain.BaseDirectory;
            string path = domain.Substring(0, domain.IndexOf("bin")) + @"JSON\";
            levels = new List<Level>();

            Level level1 = JsonConvert.DeserializeObject<Level>(
                File.ReadAllText(path + @"Level1.json"));

            levels.Add(level1);
        }
    }
}
