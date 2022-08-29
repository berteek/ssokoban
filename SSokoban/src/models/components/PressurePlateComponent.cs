using SSokoban.Core;
using SSokoban.GameStates;
using SSokoban.MapsAndSections;

namespace SSokoban.EntitiesAndComponents
{
    public enum PressurePlateType
    {
        Green,
        Purple
    }

    public class PressurePlateComponent : Component
    {
        public PressurePlateType Type { get; set; }

        public PressurePlateComponent(PressurePlateType type)
        {
            Type = type;
        }

        public void Activate()
        {
            FindInteractable("Activate");
        }

        public void Deactivate()
        {
            FindInteractable("Deactivate");
        }

        private void FindInteractable(string function)
        {
            Section section = (GameManager.GameState as PlayingState).Section;

            if (section == null)
                return;

            foreach (Entity entity in section.Entities)
            {
                PressurePlateInteractableComponent interactable = entity.GetComponent<PressurePlateInteractableComponent>();

                if (interactable == null)
                    continue;

                if (interactable.Type != Type)
                    continue;

                if (function.Equals("Activate"))
                    interactable.Activate();
                else if (function.Equals("Deactivate"))
                    interactable.Deactivate();
            }
        }
    }
}