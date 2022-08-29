using System.Collections.Generic;
using SSokoban.EntitiesAndComponents;
using SSokoban.MapsAndSections;

namespace SSokoban.Core
{
    public class Collision
    {
        public ColliderComponent Collider { get; private set; }
        public Entity Entity { get; private set; }

        public Collision(ColliderComponent collider, Entity entity)
        {
            Collider = collider;
            Entity = entity;
        }
    }

    public static class CollisionSystem
    {
        private static List<Collision> previousCollisions = new List<Collision>();
        private static List<Collision> currentCollisions = new List<Collision>();

        public static void CheckCollisions(Section section)
        {
            foreach (Entity entity1 in section.Entities)
            {
                ColliderComponent collider = entity1.GetComponent<ColliderComponent>();
                if (collider != null)
                {
                    foreach (Entity entity2 in section.Entities)
                    {
                        if (entity1 != entity2 && collider.Position == entity2.Position && collider.ZIndex == entity2.ZIndex)
                        {
                            currentCollisions.Add(new Collision(collider, entity2));
                        }
                    }
                }
            }

            foreach (Collision collision in currentCollisions)
            {
                if (!previousCollisions.Contains(collision))
                    collision.Collider.OnCollisionEntered?.Invoke(collision.Entity);

                collision.Collider.OnCollisionDetected?.Invoke(collision.Entity);
            }

            foreach (Collision collision in previousCollisions)
            {
                if (!currentCollisions.Contains(collision))
                    collision.Collider.OnCollisionExited?.Invoke(collision.Entity);
            }

            previousCollisions.Clear();
            previousCollisions.AddRange(currentCollisions);

            currentCollisions.Clear();
        }
    }
}