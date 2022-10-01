using SFML.Graphics;
using SFML.System;
using SSokoban.Core;
using SSokoban.EntitiesAndComponents;
using SSokoban.Properties;

namespace SSokoban.Utils
{
    public static class Prefabs
    {
        public static Entity WizardBoy(Vector2i position)
        {
            Entity mover = new Entity(position);
            mover.Name = "Wizard Boy";
            mover.AddComponent(new MoveComponent());
            mover.AddComponent(new SpriteComponent(new Texture(Resources.boy)));
            return mover;
        }

        public static Entity Knight(Vector2i position)
        {
            Entity mover = new Entity(position);
            mover.Name = "Knight";
            mover.AddComponent(new MoveComponent());
            mover.AddComponent(new SpriteComponent(new Texture(Resources.knight)));
            return mover;
        }

        public static Entity IO(Vector2i position)
        {
            Entity mover = new Entity(position);
            mover.Name = "IO";
            mover.AddComponent(new MoveComponent());
            mover.AddComponent(new SpriteComponent(new Texture(Resources.io)));
            return mover;
        }

        public static Entity Box(Vector2i position)
        {
            Entity box = new Entity(position);
            box.Name = "Box";
            box.AddComponent(new MoveComponent());
            box.AddComponent(new SpriteComponent(new Texture(Resources.box)));
            return box;
        }

        public static Entity Rock(Vector2i position)
        {
            Entity rock = new Entity(position);
            rock.Name = "Rock";
            rock.AddComponent(new MoveComponent());
            rock.AddComponent(new SpriteComponent(new Texture(Resources.rock_1)));
            return rock;
        }

        public static Entity Mark(Vector2i position)
        {
            Entity mark = new Entity(position);
            mark.Name = "Mark";
            mark.ZIndex = 0;
            ColliderComponent collider = new ColliderComponent(position, 1);
            collider.OnCollisionEntered += (entity) =>
            {
                if (entity.Name.Equals("Rock"))
                {
                    GameManager.RocksOnMarkCount++;
                }
            };
            collider.OnCollisionExited += (entity) =>
            {
                if (entity.Name.Equals("Rock"))
                {
                    GameManager.RocksOnMarkCount--;
                }
            };
            mark.AddComponent(new SpriteComponent(new Texture(Resources.mark_1)));
            mark.AddComponent(collider);

            return mark;
        }

        public static Entity Wall(Vector2i position)
        {
            Entity wall = new Entity(position);
            wall.Name = "Wall";
            wall.AddComponent(new SpriteComponent(new Texture(Resources.wall)));
            return wall;
        }

        public static Entity Floor(Vector2i position)
        {
            Entity floor = new Entity(position);
            floor.Name = "Floor";
            floor.AddComponent(new SpriteComponent(new Texture(Resources.floor)));
            floor.ZIndex = -1;
            return floor;
        }
    }
}