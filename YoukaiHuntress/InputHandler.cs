using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoukaiHuntress
{
    public class InputHandler
    {
        private KeyboardState currentKeyboardState, previousKeyboardState;
        private GamePadState currentGamepadState, previousGamepadState;

        public InputHandler(GamePadState gamePadState, KeyboardState keyboardState)
        {
            currentKeyboardState = keyboardState;
            currentGamepadState = gamePadState;
        }

        public void Update()
        {
            previousGamepadState = currentGamepadState;
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            currentGamepadState = GamePad.GetState(PlayerIndex.One);
        }

        public bool buttonPressed(Buttons button)
        {
            return currentGamepadState.IsButtonDown(button) && previousGamepadState.IsButtonUp(button);
        }

        public bool buttonDown(Buttons button)
        {
            return currentGamepadState.IsButtonDown(button);
        }

        public bool KeyPressed(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
        }

        public bool KeyDown(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k);
        }
        
    }
}
