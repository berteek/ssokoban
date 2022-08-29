using SFML.System;

using SSokoban.EntitiesAndComponents;

namespace SSokoban.Core
{
    public class MovementTransaction
    {
        public Entity Entity { get; private set; }
        public Vector2i Translation { get; private set; }

        public MovementTransaction(Entity entity, Vector2i translation)
        {
            Entity = entity;
            Translation = translation;
        }
    }
}