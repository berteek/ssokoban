using System.Collections.Generic;
using System.Linq;

using SFML.Window;

using SSokoban.Core;
using SSokoban.EntitiesAndComponents;
using SSokoban.Utils;

namespace SSokoban.MapsAndSections
{
    public class Section
    {
        public string MapName { get; set; }
        public Section NextSection { get; set; } = null;
        public int LevelNumber { get; set; }

        public Entity Player { get; set; }

        /*private bool greenActive = false;
        public bool GreenActive
        {
            get { return greenActive; }
            set { greenActive = value; SendActivate(value, PressurePlateType.Green); ActivateInteractables(); }
        }
        private bool purpleActive = false;
        public bool PurpleActive
        {
            get { return purpleActive; }
            set { purpleActive = value; SendActivate(value, PressurePlateType.Purple); ActivateInteractables(); }
        }

        private bool greenActiveNet = false;
        public bool GreenActiveNet { get { return greenActiveNet; } set { greenActiveNet = value; ActivateInteractables(); } }
        private bool purpleActiveNet = false;
        public bool PurpleActiveNet { get { return purpleActiveNet; } set { purpleActiveNet = value; ActivateInteractables(); } }
        */

        private List<Entity> entities;
        public List<Entity> Entities
        {
            get { return entities; }
            set { entities = value; entities.Sort((e1, e2) => e1.ZIndex.CompareTo(e2.ZIndex)); }
        }

        public int WidthInUnits { get; private set; }

        public Section(List<Entity> entities, int playerIndex, int widthInUnits)
        {
            Player = entities[playerIndex];
            Entities = entities;
            WidthInUnits = widthInUnits;
            GameManager.UNIT = VideoMode.DesktopMode.Width / WidthInUnits;
        }

        public void Sort()
        {
            entities.Sort((e1, e2) => e1.ZIndex.CompareTo(e2.ZIndex));
        }

        /*public void CheckPressurePlates()
        {
            bool greenFound = false;
            bool purpleFound = false;

            foreach (Entity entity in Entities)
            {
                if (entity.Name.Equals("Pressure Plate"))
                {
                    Entity[] foundAtPosition = Entities.Where(
                        e => e.Position == entity.Position && e.ZIndex == (entity.ZIndex + 1)
                        ).ToArray();

                    if (foundAtPosition.Length > 0)
                    {
                        PressurePlateComponent pressurePlateComponent = entity.GetComponent<PressurePlateComponent>();

                        if (pressurePlateComponent.Type == PressurePlateType.Green)
                            greenFound = true;
                        else if (pressurePlateComponent.Type == PressurePlateType.Purple)
                            purpleFound = true;
                    }
                }
            }

            if (greenFound && !GreenActive)
                GreenActive = true;
            else if (!greenFound && GreenActive)
                GreenActive = false;

            if (purpleFound && !PurpleActive)
                PurpleActive = true;
            else if (!purpleFound && PurpleActive)
                PurpleActive = false;
        }

        public void ActivateInteractables()
        {
            foreach (Entity entity in Entities)
            {
                if (entity.Name.Equals("Pressure Plate Interactable"))
                {
                    PressurePlateInteractableComponent interactable = entity.GetComponent<PressurePlateInteractableComponent>();
                    if (interactable.Type == PressurePlateType.Green)
                    {
                        if (GreenActive || GreenActiveNet)
                        {
                            interactable.Activate();
                            RestartIfPlayerOnSamePosition(entity);
                        }
                        else if (!GreenActive && !GreenActiveNet)
                            interactable.Deactivate();
                    }
                    else if (interactable.Type == PressurePlateType.Purple)
                    {
                        if (PurpleActive || PurpleActiveNet)
                        {
                            interactable.Activate();
                            RestartIfPlayerOnSamePosition(entity);
                        }
                        else if (!PurpleActive && !PurpleActiveNet)
                            interactable.Deactivate();
                    }
                }
            }
        }

        private void RestartIfPlayerOnSamePosition(Entity entity)
        {
            if (PlayerController.Player.Position == entity.Position)
            {
                GameManager.Restart();
                Network.Send("restart");
            }
        }

        public void SendActivate(bool isActive, PressurePlateType pressurePlateType)
        {
            string type = pressurePlateType.Equals(PressurePlateType.Green) ? "green" : "purple";

            if (isActive)
                Network.Send("activate " + type);
            else
                Network.Send("deactivate " + type);
        }*/
    }
}