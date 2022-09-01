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
                case "load":
                    Map map = null;
                    switch (commands[1])
                    {
                        case "init":
                            map = Maps.INIT;
                            break;
                    }
                    if (map == null)
                        return;
                    Section section = null;
                    switch (commands[2])
                    {
                        case "1":
                            section = map.Section1;
                            GameManager.CurrentSection = 1;
                            break;
                        case "2":
                            section = map.Section2;
                            GameManager.CurrentSection = 2;
                            break;
                    }
                    if (section == null)
                        return;

                    Network.Tasks += () => { GameManager.GameState = new PlayingState(section); };
                    break;
                case "imready":
                    if ((GameManager.GameState as MainMenuState).IsReady)
                    {
                        Commander.Process("load init 2");
                        Network.Send("load init 1");
                    }
                    break;
                case "disconnect":
                    MainMenuState mainMenuState = GameManager.GameState as MainMenuState;
                    if (mainMenuState != null)
                        mainMenuState.EnableReady?.Invoke(false);
                    else
                        Network.Tasks += () => { GameManager.GameState = new MainMenuState(); };
                    Network.Close();
                    break;
                /*case "activate":
                    ActivatePressurePlateInteractables(true, commands[1]);
                    break;
                case "deactivate":
                    ActivatePressurePlateInteractables(false, commands[1]);
                    break;*/
                case "requestloadnextmap":
                    Network.Tasks += () => { GameManager.CheckForNextMapLoad(); };
                    break;
                case "loadnextmap":
                    Network.Tasks += () => { GameManager.LoadNextMap(); };
                    break;
                case "rewind":
                    MoveHistory.RewindNet();
                    break;
                case "newrecord":
                    MoveHistory.CreateEmptyRecord();
                    break;
                case "restart":
                    Network.Tasks += () => { GameManager.Restart(); };
                    break;
            }
        }

        /*private static void ActivatePressurePlateInteractables(bool isActive, string type)
        {
            PlayingState playingState = GameManager.GameState as PlayingState;
            if (playingState == null)
            {
                GameManager.OnGameStateChanged += () => ActivatePressurePlateInteractables(isActive, type);
                return;
            }
            switch (type)
            {
                case "green":
                    playingState.Section.GreenActiveNet = isActive;
                    break;
                case "purple":
                    playingState.Section.PurpleActiveNet = isActive;
                    break;
            }
        }*/
    }
}