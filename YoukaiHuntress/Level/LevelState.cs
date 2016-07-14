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
        public Viewport viewport { get; set; }


        public LevelState(Map map, InputHandler inputHandler) {
            this.map = map;
            level = "L1";
            this.inputHandler = inputHandler;
            collider = new Collider();
            lives = new Lives();
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
        }

        public void Leave()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            sango = new Sango(new StateManager(content, inputHandler));
            Sango aux = (Sango)sango;
            exit = new Actors.Objects.Object(1, new Vector2(1860, 512), content.Load<Texture2D>("Exit"), 0 );
            aux.LoadContent(content);
            map.SetHearts(content);


            Actor bat = new Youkai(new StateManager(content, inputHandler));
            Actor oni = new Youkai(new StateManager(content, inputHandler));
            Actor oni2 = new Youkai(new StateManager(content, inputHandler));
            Youkai auxBat = (Youkai)bat;
            Youkai auxOni = (Youkai)oni;
            Youkai auxOni2 = (Youkai)oni2;
            auxBat.setState(new BatState(auxBat, auxBat.state));
            auxOni.setState(new OniState(auxOni, auxOni.state, new Vector2(1120, 480)));
            auxOni2.setState(new OniState(auxOni, auxOni.state, new Vector2(704, 320)));
            youkais.Add(auxBat);
            youkais.Add(auxOni);
            youkais.Add(auxOni2);

        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            sango.Update(gameTime, inputHandler);
            map.Update(inputHandler, gameTime);
            collider.Collide(map.collisionRectangles, map.spikesRectangles, sango);
            collider.Gravity(map.collisionRectangles, sango);
            lives.Update(map.hearts, sango);
            checkExit();
            foreach (Actor youkai in youkais)
            {
                youkai.Update(gameTime, inputHandler);
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
