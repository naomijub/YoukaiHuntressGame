﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoukaiHuntress.Components;

namespace YoukaiHuntress
{
    public class StateManager
    {
        public State state { get; set; }
        bool hasCalledEnter = false;
        ContentManager content;
        InputHandler inputHandler;

        public StateManager(ContentManager content, InputHandler inputHandler)
        {

            this.content = content;
            this.inputHandler = inputHandler;
        }

        public void setPrimaryState(State state)
        {
            this.state = state;
            state.LoadContent(content);
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            state.Draw(sb, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            if (!hasCalledEnter)
            {
                this.state.Enter();
                hasCalledEnter = !hasCalledEnter;
            }

            //inputHandler.Update();
            state.Update(gameTime, inputHandler);
        }

        public void ChangeState(State state)
        {
            this.state.Leave();

            this.state = state;
            this.state.LoadContent(content);
            this.state.Enter();

        }
    }
}
