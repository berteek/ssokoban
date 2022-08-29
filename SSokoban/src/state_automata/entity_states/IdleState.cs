using SFML.System;
using SFML.Window;

using SSokoban.Core;

namespace SSokoban.EntityStates
{
    public class IdleState : PlayerState
    {
        public override void HandleInput()
        {
            if (Input.GetKeyPressed(Keyboard.Key.W))
            {
                PlayerController.Move(new Vector2i(0, -1));
            }
            else if (Input.GetKeyPressed(Keyboard.Key.S))
            {
                PlayerController.Move(new Vector2i(0, 1));
            }
            else if (Input.GetKeyPressed(Keyboard.Key.A))
            {
                PlayerController.Move(new Vector2i(-1, 0));
            }
            else if (Input.GetKeyPressed(Keyboard.Key.D))
            {
                PlayerController.Move(new Vector2i(1, 0));
            }
        }
    }
}