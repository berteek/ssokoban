using System.Collections.Generic;

using SFML.System;

using SSokoban.EntitiesAndComponents;
using SSokoban.Utils;

namespace SSokoban.MapsAndSections
{
    public static class Maps
    {
        public static Map L1
        {
            get
            {
                Map map = new Map("Level 1", 1, new List<Entity>
                {
                    Prefabs.WizardBoy(new Vector2i(3, 3)),

                    Prefabs.Rock(new Vector2i(4, 3)),
                    Prefabs.Rock(new Vector2i(5, 4)),
                    Prefabs.Rock(new Vector2i(5, 5)),
                    Prefabs.Rock(new Vector2i(2, 7)),
                    Prefabs.Rock(new Vector2i(4, 7)),
                    Prefabs.Rock(new Vector2i(5, 7)),
                    Prefabs.Rock(new Vector2i(6, 7)),

                    Prefabs.Mark(new Vector2i(2, 3)),
                    Prefabs.Mark(new Vector2i(6, 4)),
                    Prefabs.Mark(new Vector2i(2, 5)),
                    Prefabs.Mark(new Vector2i(5, 6)),
                    Prefabs.Mark(new Vector2i(4, 7)),
                    Prefabs.Mark(new Vector2i(7, 7)),
                    Prefabs.Mark(new Vector2i(5, 8))
                }, 0, 7, 20);

                map.Entities.AddRange(CreateWalls(from: new Vector2i(3, 1), to: new Vector2i(7, 1)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(7, 2), to: new Vector2i(7, 6)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(8, 6), to: new Vector2i(8, 9)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(1, 2), to: new Vector2i(3, 2)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(1, 2), to: new Vector2i(1, 9)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(2, 9), to: new Vector2i(7, 9)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(2, 4), to: new Vector2i(3, 4)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(3, 5), to: new Vector2i(3, 6)));
                map.Entities.Add(Prefabs.Wall(new Vector2i(4, 5)));

                map.Entities.AddRange(CreateFloor(from: new Vector2i(2, 2), to: new Vector2i(7, 9)));

                map.Sort();

                map.NextMap = L2;

                return map;
            }
        }

        public static Map L2
        {
            get
            {
                Map map = new Map("Level 2", 2, new List<Entity>
                {
                    Prefabs.WizardBoy(new Vector2i(13, 9)),

                    Prefabs.Rock(new Vector2i(6, 3)),
                    Prefabs.Rock(new Vector2i(8, 4)),
                    Prefabs.Rock(new Vector2i(6, 5)),
                    Prefabs.Rock(new Vector2i(9, 5)),
                    Prefabs.Rock(new Vector2i(3, 8)),
                    Prefabs.Rock(new Vector2i(6, 8)),

                    Prefabs.Mark(new Vector2i(20, 7)),
                    Prefabs.Mark(new Vector2i(21, 7)),
                    Prefabs.Mark(new Vector2i(20, 8)),
                    Prefabs.Mark(new Vector2i(21, 8)),
                    Prefabs.Mark(new Vector2i(20, 9)),
                    Prefabs.Mark(new Vector2i(21, 9))
                }, 0, 6, 23);

                map.Entities.AddRange(CreateWalls(from: new Vector2i(5, 1), to: new Vector2i(9, 1)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(5, 2), to: new Vector2i(5, 4)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(9, 2), to: new Vector2i(9, 4)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(3, 5), to: new Vector2i(3, 6)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(3, 4), to: new Vector2i(4, 4)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(1, 6), to: new Vector2i(2, 6)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(1, 7), to: new Vector2i(1, 9)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(2, 9), to: new Vector2i(5, 9)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(5, 10), to: new Vector2i(5, 11)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(6, 11), to: new Vector2i(11, 11)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(12, 9), to: new Vector2i(12, 11)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(13, 10), to: new Vector2i(14, 10)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(14, 9), to: new Vector2i(17, 9)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(17, 10), to: new Vector2i(22, 10)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(22, 6), to: new Vector2i(22, 9)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(17, 6), to: new Vector2i(21, 6)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(11, 7), to: new Vector2i(17, 7)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(11, 4), to: new Vector2i(11, 6)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(9, 4), to: new Vector2i(10, 4)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(5, 6), to: new Vector2i(5, 7)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(7, 6), to: new Vector2i(9, 7)));
                map.Entities.AddRange(CreateWalls(from: new Vector2i(7, 9), to: new Vector2i(10, 9)));

                //map.Entities.AddRange(CreateFloor(from: new Vector2i(2, 2), to: new Vector2i(7, 9)));

                map.Sort();

                return map;
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
                    return L1;
            }

            return null;
        }
    }
}