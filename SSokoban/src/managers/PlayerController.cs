using SFML.System;
using SFML.Window;

using SSokoban.EntitiesAndComponents;

namespace SSokoban.Core
{
    public static class PlayerController
    {
        public static Entity Player { get; set; }

        public static void HandleInput()
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

        public static void Move(Vector2i direction)
        {
            MoveComponent moveComponent = Player.GetComponent<MoveComponent>();

            if (moveComponent == null)
                return;

            moveComponent.Move(direction);
        }
    }
}