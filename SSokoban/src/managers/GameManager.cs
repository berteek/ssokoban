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

        public static int RocksOnMarkCount { get; set; } = 0;

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
                    gameState = new MapTitleState(playingState.Map.Name,
                        () =>
                        {
                            gameState = value;
                            OnGameStateChanged?.Invoke();
                        });
                }
                else
                {
                    gameState = value;
                }
                OnGameStateChanged?.Invoke();
            }
        }

        public static void LoadNextMap()
        {
            PlayingState playingState = GameState as PlayingState;

            if (playingState == null)
                return;

            if (playingState.Map.NextMap == null)
            {
                GameState = new WinState();
                Network.Send("youlost");
            }
            else
            {
                CurrentLevel = playingState.Map.NextMap.LevelNumber;
                GameState = new PlayingState(playingState.Map.NextMap);
            }
        }

        public static void LoadMap(Map map)
        {
            GameState = new PlayingState(map);
            CurrentLevel = map.LevelNumber;
        }

        public static void Restart()
        {
            Map map = Maps.MapByNumber(CurrentLevel);
            GameState = new PlayingState(map);
        }

        public static void CheckIfAllRocksAreInPlaces()
        {
            PlayingState playingState = GameState as PlayingState;
            if (playingState == null)
                return;

            if (RocksOnMarkCount == playingState.Map.RocksNumber)
                LoadNextMap();
        }
    }
}