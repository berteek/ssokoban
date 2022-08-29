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
            rock.AddComponent(new SpriteComponent(new Texture(Resources.rock)));
            return rock;
        }

        public static Entity Escape(Vector2i position)
        {
            Entity escape = new Entity(position);

            escape.Name = "Escape";
            escape.ZIndex = 0;

            escape.AddComponent(new SpriteComponent(new Texture(Resources.escape)));

            EscapeComponent escapeComponent = new EscapeComponent();
            ColliderComponent collider = new ColliderComponent(position, 1);
            collider.OnCollisionEntered += (entity) =>
            {
                if (PlayerController.Player == entity)
                {
                    escapeComponent.PlayerOnEscape = true;
                    GameManager.LoadNextMap();
                    GameManager.RequestLoadNextMap();
                }
            };
            collider.OnCollisionExited += (entity) =>
            {
                if (PlayerController.Player == entity)
                    escapeComponent.PlayerOnEscape = false;
            };
            escape.AddComponent(escapeComponent);
            escape.AddComponent(collider);

            return escape;
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

        /*
        public static Entity PressurePlate(Vector2i position, PressurePlateType type)
        {
            Entity plate = new Entity(position);
            plate.Name = "Pressure Plate";
            Texture texture = null;
            if (type == PressurePlateType.Green)
                texture = new Texture(Resources.pressure_plate_green);
            else if (type == PressurePlateType.Purple)
                texture = new Texture(Resources.pressure_plate_purple);
            SpriteComponent spriteComponent = new SpriteComponent(texture);
            plate.AddComponent(spriteComponent);
            plate.AddComponent(new PressurePlateComponent(type));
            plate.ZIndex = 0;
            return plate;
        }

        public static Entity PressurePlateInteractable(Vector2i position, PressurePlateType type)
        {
            Entity interactable = new Entity(position);
            interactable.Name = "Pressure Plate Interactable";
            Texture texture = null;
            if (type == PressurePlateType.Green)
                texture = new Texture(Resources.pressure_plate_interactable_not_active_green);
            else if (type == PressurePlateType.Purple)
                texture = new Texture(Resources.pressure_plate_interactable_not_active_purple);
            SpriteComponent spriteComponent = new SpriteComponent(texture);
            interactable.AddComponent(spriteComponent);
            interactable.AddComponent(new PressurePlateInteractableComponent(type, spriteComponent));
            interactable.ZIndex = 0;
            return interactable;
        }*/
    }
}