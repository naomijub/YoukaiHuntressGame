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

namespace YoukaiHuntress.Level
{
    public class LevelState : State
    {
        public Map map { get; set; }
        public string level { get; set; }
        public Actor sango { get; set; }
        public InputHandler inputHandler;
        Collider collider;


        public LevelState(Map map, InputHandler inputHandler) {
            this.map = map;
            level = "L1";
            this.inputHandler = inputHandler;
            collider = new Collider();
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            map.Draw(sb, new Vector2(3, 20));
            sango.Draw(sb, gameTime);
        }

        public void Enter()
        {
            map.ChangeLevel(level);
            map.Update(sango.origin);
        }

        public void Leave()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            sango = new Sango(new StateManager(content, inputHandler));
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            sango.Update(gameTime, inputHandler);
            map.Update(sango.origin);
            collider.Collide(map.collisionRectangles,sango);
            //collider.Gravity(map.collisionRectangles, sango);
        }
    }
}
