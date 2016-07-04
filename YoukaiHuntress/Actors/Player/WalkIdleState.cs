using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YoukaiHuntress.Components;

namespace YoukaiHuntress.Actors.Player
{
    class WalkIdleState : State
    {
        public enum ActorState { idle, left, right}
        ActorState actorState; 
        public Sango sango { get; set; }
        public StateManager state { get; set; }
        float currentTime = 0f;

        public WalkIdleState(Sango sango, StateManager state) {
            this.sango = sango;
            this.state = state;
            actorState = ActorState.idle;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            //sb.Draw(sango.texture, sango.position, sango.source, Color.White);
            sb.Draw(sango.texture, sango.position, null, sango.source, null, 0.0f, null, Color.White, sango.effect, 0.0f);
            
        }

        public void Enter()
        {
            sango.area = new Point(30, 42);
            sango.spriteVariation = 0;
            sango.spriteLength = 8;
            actorState = ActorState.idle;
        }

        public void Leave()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            sango.texture = content.Load<Texture2D>("Sango");
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            inputHandler.Update();
            if (sango.hasJumped)
            {
                Jump(gameTime);
            }
            else {
                nextMove(inputHandler, gameTime);
            }
            SetCollisionReagion();
            Timer(gameTime);
            SetSource();
            SetOrigin();
            
        }

        private void SetOrigin()
        {
            sango.origin = sango.position + new Vector2(15, 21);
        }

        private void nextMove(InputHandler inputHandler, GameTime gameTime)
        {
            bool keyLeft = inputHandler.KeyDown(Keys.Left);
            bool keyRight = inputHandler.KeyDown(Keys.Right);

            if (keyLeft && inputHandler.KeyDown(Keys.Space)) {
                sango.effect = SpriteEffects.FlipHorizontally;
                sango.hasJumped = true;
                sango.jumpVector = new Vector2(-2, -15);
            } else if (keyRight && inputHandler.KeyDown(Keys.Space)) {
                sango.effect = SpriteEffects.None;
                sango.hasJumped = true;
                sango.jumpVector = new Vector2(+2, -15);
            }
            else if (inputHandler.KeyDown(Keys.Left) || inputHandler.buttonDown(Buttons.DPadLeft))
            {
                sango.nextMoveX = -2;
                sango.effect = SpriteEffects.FlipHorizontally;
                sango.hasJumped = false;
                sango.jumpVector = Vector2.Zero;
                actorState = ActorState.left;
            }
            else if (inputHandler.KeyDown(Keys.Right) || inputHandler.buttonDown(Buttons.DPadRight))
            {
                sango.nextMoveX = +2;
                sango.effect = SpriteEffects.None;
                sango.hasJumped = false;
                sango.jumpVector = Vector2.Zero;
                actorState = ActorState.right;
            }
            else if (inputHandler.KeyDown(Keys.Space) || inputHandler.buttonDown(Buttons.A))
            {
                sango.jumpVector = new Vector2(0, -15);
                sango.hasJumped = true;
            }
            else if (!inputHandler.KeyDown(Keys.Right) && !inputHandler.KeyDown(Keys.Left)) {
                sango.nextMoveX = sango.nextMoveY = 0;
                sango.jumpVector = Vector2.Zero;
                sango.hasJumped = false;
                actorState = ActorState.idle;
            }

            if (inputHandler.KeyPressed(Keys.A)) {
                this.state.ChangeState(new AttackState(sango, this.state));
            }

        }

        public void Jump(GameTime gameTime) {
            if (sango.jumpVector.Y < 15)
            {
                sango.jumpVector = new Vector2(sango.jumpVector.X, sango.jumpVector.Y + 1);
                sango.grounded = false;
            }
            else {
                sango.hasJumped = false;
            }
        }

        public void SetCollisionReagion() {
            Vector2 auxPosition = sango.position + new Vector2(10, 0);
            Point auxArea = new Point(20, 42);
            sango.fullCollision = new Rectangle(auxPosition.ToPoint(), auxArea);
            if (!sango.hasJumped)
            {
                Vector2 auxPosX = auxPosition + new Vector2(sango.nextMoveX, 0);
                Vector2 auxPosY = auxPosition + new Vector2(0, sango.nextMoveY);
                sango.xCollision = new Rectangle(auxPosX.ToPoint(), auxArea);
                sango.yCollision = new Rectangle(auxPosY.ToPoint(), auxArea);
            }
            else {
                Vector2 auxPosX = auxPosition + new Vector2(sango.jumpVector.X, 0);
                Vector2 auxPosY = auxPosition + new Vector2(0, sango.jumpVector.Y);
                sango.xCollision = new Rectangle(auxPosX.ToPoint(), auxArea);
                sango.yCollision = new Rectangle(auxPosY.ToPoint(), auxArea);
            }
        }

        private void SetSource()
        {
            if (actorState == ActorState.idle)
            {
                sango.source = new Rectangle(0, 10, 30, 42);
            }
            else {
                walk();       
            }
        }

        public void walk() {

            int auxVar = (30 * sango.spriteVariation);
            sango.source = new Rectangle(auxVar, 68, sango.area.X, sango.area.Y);
        }
        
        public void Timer(GameTime gameTime) {
            float duration = 0.1f;

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime >= duration)
            {
                sango.spriteVariation++;
                currentTime -= duration; 
            }
            if (sango.spriteVariation >= sango.spriteLength)
            {
                sango.spriteVariation = 0;
            }
        }
    }
}
