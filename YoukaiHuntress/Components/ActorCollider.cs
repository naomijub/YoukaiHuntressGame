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
using Microsoft.Xna.Framework.Audio;

namespace YoukaiHuntress.Components
{
    public class ActorCollider
    {
        SoundEffect hitSnd;
        SoundEffectInstance hitSndInstance;

        public ActorCollider() { }

        public void LoadContent(ContentManager content)
        {
            hitSnd = content.Load<SoundEffect>("Hit");
            hitSndInstance = hitSnd.CreateInstance();
        }

        public void collide(Actor sango, IList<Actor> youkais) {
            Sango aux = (Sango)sango;
            for (int i = 0; i < youkais.Count; i++) {
                if (sango.fullCollision.Intersects(youkais[i].fullCollision)) {
                    aux.lives -= 1;
                    aux.position = new Vector2(aux.position.X -16, aux.position.Y);
                }
                if (aux.boomerang != null) {
                    if (aux.boomerang.fullCollision.Intersects(youkais[i].fullCollision)) {
                        hitSndInstance.Play();
                        youkais.Remove(youkais[i]);
                    }
                }
            }
        }
    }
}
