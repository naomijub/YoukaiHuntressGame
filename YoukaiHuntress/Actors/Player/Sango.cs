using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using YoukaiHuntress.Components;

namespace YoukaiHuntress.Actors.Player
{
    public class Sango : Actor
    {
        public StateManager state { get; set; }
        public Vector2 jumpVector { get; set; }
        public bool hasJumped { get; set; }
        public bool grounded { get; set; }
        public Boomerang boomerang { get; set; }
        public int lives { get; set; }
        Texture2D heart;

        public Sango(StateManager state) : base() {
            this.state = state;
            this.state.setPrimaryState(new WalkIdleState(this, this.state));
            position = new Vector2(36,534);
            origin = position + new Vector2(15, 21);
            hasJumped = false;
            grounded = true;
            lives = 3;
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            state.Draw(sb, gameTime);
            drawLives(sb);
        }

        public override void Update(GameTime gameTime, InputHandler input)
        {
            Camera2D.SetPos(position);
            state.Update(gameTime);
        }

        public bool isAlive() {
            if (lives <= 0) {
                return false;
            }
            return true;
        }

        public void LoadContent(ContentManager content) {
            heart = content.Load<Texture2D>("heart");
            texture = content.Load<Texture2D>("Sango");
        }

        private void drawLives(SpriteBatch sb)
        {
            Rectangle souceFace = new Rectangle(0, 330, 30, 33);
            Vector2 livesVect = determineLivesPosition();
            sb.Draw(texture, livesVect, souceFace, Color.White);
            for (int i = 0; i < lives; i++) {
                int x = (int)livesVect.X + 45 + (i * 20);
                Vector2 auxVec = new Vector2(x, 15);
                sb.Draw(heart, auxVec, Color.White);
            }
        }

        private Vector2 determineLivesPosition()
        {
            if (position.X < 480)
            {
                return new Vector2(10, 10);
            }
            else if (position.X > 1440)
            {
                return new Vector2(970, 10);
            }
            return new Vector2(position.X - 470, 10);
            
        }
    }
}
