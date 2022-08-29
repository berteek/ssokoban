using System.Collections.Generic;

using SFML.System;

using SSokoban.EntitiesAndComponents;
using SSokoban.Utils;

namespace SSokoban.MapsAndSections
{
    public static class Maps
    {
        public static Map INIT
        {
            get
            {
                Section section1 = new Section(new List<Entity>
                {
                    Prefabs.WizardBoy(new Vector2i(3, 3)),

                    Prefabs.Rock(new Vector2i(6, 3)),

                    /*
                    Prefabs.PressurePlate(new Vector2i(6, 2), PressurePlateType.Purple),
                    Prefabs.PressurePlate(new Vector2i(6, 3), PressurePlateType.Green),

                    Prefabs.PressurePlateInteractable(new Vector2i(5, 2), PressurePlateType.Purple),
                    Prefabs.PressurePlateInteractable(new Vector2i(5, 3), PressurePlateType.Purple),
                    Prefabs.PressurePlateInteractable(new Vector2i(5, 4), PressurePlateType.Purple),

                    Prefabs.PressurePlateInteractable(new Vector2i(7, 2), PressurePlateType.Green),
                    Prefabs.PressurePlateInteractable(new Vector2i(7, 3), PressurePlateType.Green),
                    Prefabs.PressurePlateInteractable(new Vector2i(7, 4), PressurePlateType.Green),

                    Prefabs.PressurePlate(new Vector2i(8, 2), PressurePlateType.Green),
                    Prefabs.PressurePlate(new Vector2i(8, 3), PressurePlateType.Green),
                    Prefabs.PressurePlate(new Vector2i(8, 4), PressurePlateType.Green),

                    Prefabs.PressurePlateInteractable(new Vector2i(9, 2), PressurePlateType.Purple),
                    Prefabs.PressurePlateInteractable(new Vector2i(9, 3), PressurePlateType.Purple),
                    Prefabs.PressurePlateInteractable(new Vector2i(9, 4), PressurePlateType.Purple),
                    */

                    Prefabs.Escape(new Vector2i(11, 3))
                }, 0, 15);

                section1.Entities.AddRange(CreateWalls(from: new Vector2i(1, 1), to: new Vector2i(13, 1)));
                section1.Entities.AddRange(CreateWalls(from: new Vector2i(13, 2), to: new Vector2i(13, 5)));
                section1.Entities.AddRange(CreateWalls(from: new Vector2i(1, 5), to: new Vector2i(12, 5)));
                section1.Entities.AddRange(CreateWalls(from: new Vector2i(1, 2), to: new Vector2i(1, 4)));

                section1.Entities.AddRange(CreateFloor(from: new Vector2i(2, 2), to: new Vector2i(12, 4)));

                Section section2 = new Section(new List<Entity>
                {
                    Prefabs.Knight(new Vector2i(3, 3)),

                    /*Prefabs.PressurePlate(new Vector2i(2, 4), PressurePlateType.Green),

                    Prefabs.PressurePlate(new Vector2i(5, 4), PressurePlateType.Purple),
                    Prefabs.PressurePlate(new Vector2i(6, 4), PressurePlateType.Purple),
                    */
                    Prefabs.Rock(new Vector2i(5, 4)),
                    Prefabs.Rock(new Vector2i(6, 4)),

                    Prefabs.Wall(new Vector2i(8, 2)),
                    Prefabs.Wall(new Vector2i(8, 4)),

                    /*
                    Prefabs.PressurePlateInteractable(new Vector2i(8, 3), PressurePlateType.Green),

                    Prefabs.PressurePlateInteractable(new Vector2i(9, 2), PressurePlateType.Purple),
                    Prefabs.PressurePlateInteractable(new Vector2i(9, 3), PressurePlateType.Purple),
                    Prefabs.PressurePlateInteractable(new Vector2i(9, 4), PressurePlateType.Purple),
                    */

                    Prefabs.Escape(new Vector2i(11, 3))
                }, 0, 15);

                section2.Entities.AddRange(CreateWalls(from: new Vector2i(1, 1), to: new Vector2i(13, 1)));
                section2.Entities.AddRange(CreateWalls(from: new Vector2i(13, 2), to: new Vector2i(13, 5)));
                section2.Entities.AddRange(CreateWalls(from: new Vector2i(1, 5), to: new Vector2i(12, 5)));
                section2.Entities.AddRange(CreateWalls(from: new Vector2i(1, 2), to: new Vector2i(1, 4)));

                section2.Entities.AddRange(CreateFloor(from: new Vector2i(2, 2), to: new Vector2i(12, 4)));

                section1.Sort();
                section2.Sort();

                return new Map("LEVEL 1", 1, section1, section2);
            }
        }

        private static List<Entity> CreateWalls(Vector2i from, Vector2i to)
        {
            List<Entity> walls = new List<Entity>();

            for (int i = from.X; i <= to.X; i++)
                for (int j = from.Y; j <= to.Y; j++)
                    walls.Add(Prefabs.Wall(new Vector2i(i, j)));

            return walls;
        }

        private static List<Entity> CreateFloor(Vector2i from, Vector2i to)
        {
            List<Entity> floor = new List<Entity>();

            for (int i = from.X; i <= to.X; i++)
                for (int j = from.Y; j <= to.Y; j++)
                    floor.Add(Prefabs.Floor(new Vector2i(i, j)));

            return floor;
        }

        public static Map MapByNumber(int number)
        {
            switch (number)
            {
                case 1:
                    return INIT;
            }

            return null;
        }
    }
}