using SFML.System;

using SSokoban.EntitiesAndComponents;
using SSokoban.EntityStates;

namespace SSokoban.Core
{
    public static class PlayerController
    {
        public static Entity Player { get; set; }

        public static PlayerState PlayerState { get; set; } = new IdleState();

        public static void Move(Vector2i direction)
        {
            MoveComponent moveComponent = Player.GetComponent<MoveComponent>();

            if (moveComponent == null)
                return;
            
            moveComponent.Move(direction);
        }
    }
}