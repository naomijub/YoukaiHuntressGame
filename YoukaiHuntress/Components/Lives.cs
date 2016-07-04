using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoukaiHuntress.Actors;
using YoukaiHuntress.Actors.Player;

namespace YoukaiHuntress.Components
{
    public class Lives
    {
        public Lives() { }

        public void Update(IList<Heart> hearts, Actor sango) {
            for (int i = 0; i < hearts.Count; i++) {
                if (hearts[i].isAvailable) {
                    if (hearts[i].Update(sango.fullCollision)) {
                        Sango aux = (Sango)sango;
                        aux.lives += 1;
                    }
                }
            }
        }
    }
}
