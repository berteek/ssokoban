using System;

using SFML.System;

namespace SSokoban.EntitiesAndComponents
{
    public class ColliderComponent : Component
    {
        public Vector2i Position { get; set; }
        public int ZIndex { get; set; }

        public Action<Entity> OnCollisionDetected { get; set; }

        public Action<Entity> OnCollisionEntered { get; set; }

        public Action<Entity> OnCollisionExited { get; set; }

        public ColliderComponent(Vector2i position, int zIndex)
        {
            Position = position;
            ZIndex = zIndex;
        }
    }
}