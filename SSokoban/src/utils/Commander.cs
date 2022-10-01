using System.Text.RegularExpressions;

using SSokoban.GameStates;
using SSokoban.MapsAndSections;
using SSokoban.Core;

namespace SSokoban.Utils
{
    public static class Commander
    {
        public static void Process(string message)
        {
            message = message.Trim();
            string[] commands = Regex.Split(message, @"\s+");

            switch (commands[0])
            {
                case "start":
                    Network.Tasks += () => { GameManager.GameState = new PlayingState(Maps.L1); };
                    break;
                case "imready":
                    if ((GameManager.GameState as MainMenuState).IsReady)
                    {
                        Commander.Process("start");
                        Network.Send("start");
                    }
                    break;
                case "youlost":
                    Network.Tasks += () => { GameManager.GameState = new LoseState(); };
                    break;
                case "disconnect":
                    MainMenuState mainMenuState = GameManager.GameState as MainMenuState;
                    if (mainMenuState != null)
                        mainMenuState.EnableReady?.Invoke(false);
                    else
                        Network.Tasks += () => { GameManager.GameState = new MainMenuState(); };
                    Network.Close();
                    break;
            }
        }
    }
}