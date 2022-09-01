using System.Linq;
using SSokoban.EntitiesAndComponents;
using SSokoban.GameStates;
using SSokoban.MapsAndSections;
using SSokoban.Utils;

namespace SSokoban.Core
{
    public static class GameManager
    {
        public static Game Game { get; set; }

        public static int CurrentLevel { get; set; } = 1;
        public static int CurrentSection { get; set; }

        public static float dt;

        public static float UNIT { get; set; }

        public static event System.Action OnGameStateChanged;

        private static GameState gameState;
        public static GameState GameState
        {
            get { return gameState; }
            set
            {
                PlayingState playingState = value as PlayingState;

                if (playingState != null)
                {
                    gameState = new MapTitleState(playingState.Section.MapName,
                        () =>
                        {
                            gameState = value;
                            OnGameStateChanged?.Invoke();
                            Network.Send("update");
                        });
                }
                else
                {
                    gameState = value;
                }
                OnGameStateChanged?.Invoke();
            }
        }

        public static void RequestLoadNextMap()
        {
            Network.Send("requestloadnextmap");
        }

        public static void CheckForNextMapLoad()
        {
            PlayingState playingState = GameState as PlayingState;

            if (playingState == null)
                return;

            Entity escape = playingState.Section.Entities.FirstOrDefault<Entity>((entity) => entity.GetComponent<EscapeComponent>() != null);
            if (escape == null)
                return;

            if (escape.GetComponent<EscapeComponent>().PlayerOnEscape)
            {
                Network.Send("loadnextmap");

                if (playingState.Section.NextSection == null)
                {
                    GameState = new WinState();
                }
                else
                {
                    CurrentLevel = playingState.Section.NextSection.LevelNumber;
                    GameState = new PlayingState(playingState.Section.NextSection);
                }
            }
        }

        public static void LoadNextMap()
        {
            PlayingState playingState = GameState as PlayingState;

            if (playingState == null)
                return;

            if (playingState.Section.NextSection == null)
            {
                GameState = new WinState();
            }
            else
            {
                CurrentLevel = playingState.Section.NextSection.LevelNumber;
                GameState = new PlayingState(playingState.Section.NextSection);
            }
        }

        public static void LoadMap(Map map)
        {
            GameState = new PlayingState(map.Section1);
            CurrentLevel = map.LevelNumber;
        }

        public static void Restart()
        {
            Map map = Maps.MapByNumber(CurrentLevel);
            Section section = CurrentSection == 1 ? map.Section1 : map.Section2;
            GameState = new PlayingState(section);
        }
    }
}