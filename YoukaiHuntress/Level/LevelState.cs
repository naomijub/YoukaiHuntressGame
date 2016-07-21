using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using YoukaiHuntress.Components;
using YoukaiHuntress.LevelManager;
using YoukaiHuntress.Actors.Player;
using YoukaiHuntress.Actors;
using YoukaiHuntress.Actors.Enemy;
using Microsoft.Xna.Framework.Audio;

namespace YoukaiHuntress.Level
{
    public class LevelState : State
    {
        public Map map { get; set; } 
        public string level { get; set; }
        public Actor sango { get; set; }
        public Actor exit { get; set; }
        public IList<Actor> youkais { get; set; }
        Lives lives;
        public InputHandler inputHandler;
        Collider collider;
        ActorCollider actCollider;
        public Viewport viewport { get; set; }
        SoundEffect levelSnd;
        SoundEffectInstance levelSndInstance;


        public LevelState(Map map, InputHandler inputHandler) {
            this.map = map;
            level = "L1";
            this.inputHandler = inputHandler;
            collider = new Collider();
            lives = new Lives();
            actCollider = new ActorCollider();
            youkais = new List<Actor>();
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            map.Draw(sb, gameTime);
            sango.Draw(sb, gameTime);
            exit.Draw(sb, gameTime);
            foreach (Actor youkai in youkais) {
                youkai.Draw(sb, gameTime);
            }
        }

        public void Enter()
        {
            map.ChangeLevel(level);
            levelSndInstance = levelSnd.CreateInstance();
            levelSndInstance.Volume = 0.03f;
            levelSndInstance.Play();
        }

        public void Leave()
        {
            levelSndInstance.Stop();
        }

        public void LoadContent(ContentManager content)
        {
            sango = new Sango(new StateManager(content, inputHandler));
            Sango aux = (Sango)sango;
            exit = new Actors.Objects.Object(1, new Vector2(1860, 512), content.Load<Texture2D>("Exit"), 0 );
            aux.LoadContent(content);
            map.SetHearts(content);
            levelSnd = content.Load<SoundEffect>("No Victim");
            actCollider.LoadContent(content);

            Actor bat = new Youkai(new StateManager(content, inputHandler));
            Actor oni = new Youkai(new StateManager(content, inputHandler));
            Actor oni2 = new Youkai(new StateManager(content, inputHandler));
            Actor oni3 = new Youkai(new StateManager(content, inputHandler));
            Actor Gyu = new Youkai(new StateManager(content, inputHandler));
            Youkai auxBat = (Youkai)bat;
            Youkai auxOni = (Youkai)oni;
            Youkai auxOni2 = (Youkai)oni2;
            Youkai auxOni3 = (Youkai)oni3;
            Youkai auxGyu = (Youkai)Gyu;
            auxBat.setState(new BatState(auxBat, auxBat.state));
            auxOni.setState(new OniState(auxOni, auxOni.state, new Vector2(1120, 480)));
            auxOni2.setState(new OniState(auxOni2, auxOni2.state, new Vector2(704, 320)));
            auxOni3.setState(new OniState(auxOni3, auxOni3.state, new Vector2(1728, 288)));
            auxGyu.setState(new GyuState(auxGyu, auxGyu.state, new Vector2(1550, 501)));
            youkais.Add(auxBat);
            youkais.Add(auxOni);
            youkais.Add(auxOni2);
            youkais.Add(auxOni3);
            youkais.Add(auxGyu);
        }
        
        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            sango.Update(gameTime, inputHandler);
            map.Update(inputHandler, gameTime);
            collider.Collide(map.collisionRectangles, map.spikesRectangles, sango);
            collider.Gravity(map.collisionRectangles, sango);
            lives.Update(map.hearts, sango);
            checkExit();
            actCollider.collide(sango, youkais);
            foreach (Actor youkai in youkais)
            {
                youkai.Update(gameTime, inputHandler);
                Youkai aux = (Youkai)youkai;
                if (aux.state.state.GetType() == typeof(GyuState)) {
                    GyuState auxState = (GyuState)aux.state.state;
                    auxState.checkDist(sango);
                }
            }
           
        }

        private void checkExit()
        {
            if (sango.fullCollision.Intersects(exit.fullCollision)) {
                Console.WriteLine("You win");
                
            }
        }
    }
}
