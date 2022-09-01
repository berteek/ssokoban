using System;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

using SSokoban.GameStates;
using SSokoban.Interface;
using SSokoban.Utils;

namespace SSokoban.Core
{
    public class Game
    {
        public uint windowWidth { get; private set; }
        public uint windowHeight { get; private set; }
        public string windowTitle { get; private set; }
        private RenderWindow window;
        public static Color clearColor = new Color(0, 0, 0);

        private Clock clock = new Clock();

        public Game(uint windowWidth, uint windowHeight, string windowTitle)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.windowTitle = windowTitle;

            SetupWindow();
            SetupStateAutomata();
            SetupInputSystem();
        }

        private void SetupWindow()
        {
            window = new RenderWindow(new VideoMode(windowWidth, windowHeight), windowTitle, Styles.Titlebar);

            window.Closed += (object sender, EventArgs e) => Close();
            window.Size /= 2;
            window.SetKeyRepeatEnabled(false);
            window.SetFramerateLimit(60);
            window.SetMouseCursorVisible(true);
        }

        private void SetupStateAutomata()
        {
            GameManager.Game = this;

            SetupTitle();

            GameManager.GameState = new MainMenuState();
        }

        private void SetupTitle()
        {
            TextPiece mapTitle = new TextPiece("", 100);
            mapTitle.Text.FillColor = new Color(230, 230, 230);
            mapTitle.AddTrait(new CenterTextTrait());
        }

        private void SetupInputSystem()
        {
            window.KeyPressed += (sender, e) => Input.Keys.Add(e.Code, KeyState.Pressed);
            window.KeyReleased += (sender, e) => Input.Keys.Add(e.Code, KeyState.Released);
        }

        public void Run()
        {
            while (window.IsOpen)
            {
                GameManager.dt = clock.Restart().AsSeconds();
                window.SetTitle($"{windowTitle} [FPS: {Math.Round(1 / GameManager.dt, 2)}]");

                Network.Tasks?.Invoke();
                Network.Tasks = () => {};
                
                Input.Keys.Clear();
                window.DispatchEvents();

                GameManager.GameState.Update();

                window.Clear(clearColor);
                GameManager.GameState.Draw(window);
                window.Display();
            }
        }

        public void Close()
        {
            Network.Close();
            window.Close();
        }
    }
}