using SFML.Graphics;
using SSokoban.Properties;
using SSokoban.Utils;

namespace SSokoban.EntitiesAndComponents
{
    public class PressurePlateInteractableComponent : Component
    {
        public PressurePlateType Type { get; private set; }

        private SpriteComponent spriteComponent;

        public PressurePlateInteractableComponent(PressurePlateType type, SpriteComponent spriteComponent)
        {
            Type = type;
            this.spriteComponent = spriteComponent;
        }

        public void Activate()
        {
            if (Type == PressurePlateType.Green)
            {
                spriteComponent.Sprite.Texture = new Texture(Resources.pressure_plate_interactable_active_green);
            }
            else if (Type == PressurePlateType.Purple)
            {
                spriteComponent.Sprite.Texture = new Texture(Resources.pressure_plate_interactable_active_purple);
            }

            entity.ZIndex = 1;
        }

        public void Deactivate()
        {
            if (Type == PressurePlateType.Green)
                spriteComponent.Sprite.Texture = new Texture(Resources.pressure_plate_interactable_not_active_green);
            else if (Type == PressurePlateType.Purple)
                spriteComponent.Sprite.Texture = new Texture(Resources.pressure_plate_interactable_not_active_purple);
            
            entity.ZIndex = 0;
        }
    }
}