using System;

using SFML.System;

using SSokoban.Core;

namespace SSokoban.EntitiesAndComponents
{
    public class MoveComponent : Component
    {
        public void Move(Vector2i direction)
        {
            MovementTransactionSystem.Submit(new MovementTransaction(entity, direction));
        }
    }
}