using System.Collections.Generic;
using SFML.Window;
using SSokoban.Core;
using SSokoban.EntitiesAndComponents;

namespace SSokoban.MapsAndSections
{
    public class Map
    {
        public string Name { get; private set; }
        public int LevelNumber { get; private set; }

        public Map NextMap { get; set; }

        public Entity Player { get; set; }
        public int RocksNumber { get; set; }

        private List<Entity> entities;
        public List<Entity> Entities
        {
            get { return entities; }
            set { entities = value; entities.Sort((e1, e2) => e1.ZIndex.CompareTo(e2.ZIndex)); }
        }

        public Map(string name, int levelNumber, List<Entity> entities, int playerIndex, int rocksNumber, int widthInUnits)
        {
            Name = name;
            LevelNumber = levelNumber;
            Player = entities[playerIndex];
            RocksNumber = rocksNumber;
            Entities = entities;
            GameManager.UNIT = VideoMode.DesktopMode.Width / widthInUnits;
        }

        public void Sort()
        {
            entities.Sort((e1, e2) => e1.ZIndex.CompareTo(e2.ZIndex));
        }
    }
}