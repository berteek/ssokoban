namespace SSokoban.MapsAndSections
{
    public class Map
    {
        public string Name { get; private set; }
        public int LevelNumber { get; private set; }

        public Section Section1 { get; private set; }
        public Section Section2 { get; private set; }

        public Map(string name, int levelNumber, Section section1, Section section2)
        {
            Name = name;
            LevelNumber = levelNumber;
            Section1 = section1;
            Section2 = section2;

            Section1.MapName = Name;
            Section2.MapName = Name;

            Section1.LevelNumber = LevelNumber;
            Section2.LevelNumber = LevelNumber;
        }
    }
}