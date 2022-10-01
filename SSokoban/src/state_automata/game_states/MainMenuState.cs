using SFML.Window;
using SFML.Graphics;
using SFML.System;

using System;

using SSokoban.Interface;
using SSokoban.Utils;
using SSokoban.Core;
using SSokoban.Properties;

namespace SSokoban.GameStates
{
    public class MainMenuState : GameState
    {
        public UI UI { get; set; }

        public UI mainMenuUI { get; set; }
        public UI connectUI { get; set; }
        public UI changePortUI { get; set; }

        private Sprite background;

        private TextPiece connectionText;
        private TextPiece isReadyText;

        public Action<bool> EnableReady;
        private bool isReady;
        public bool IsReady
        {
            get { return isReady; }
            set { isReady = value; isReadyText.Text.DisplayedString = isReady ? "Ready" : "Not ready"; }
        }

        private uint charSize = 50;

        public MainMenuState()
        {
            SetupMainMenuUI();
            SetupConnectUI();
            SetupChangePortUI();

            UI = mainMenuUI;

            background = new Sprite(new Texture(Resources.background3));
            background.Scale = new Vector2f(GameManager.Game.windowWidth / background.GetGlobalBounds().Width,
                                            GameManager.Game.windowHeight / background.GetGlobalBounds().Height);
        }

        private void SetupMainMenuUI()
        {
            mainMenuUI = new UI();

            TextPiece gameTitleLayer1 = new TextPiece("SSokoban", 200);
            TextPiece gameTitleLayer2 = new TextPiece("SSokoban", 200);

            gameTitleLayer1.Text.FillColor = new Color(51, 21, 65);
            gameTitleLayer2.Text.FillColor = new Color(134, 193, 163);

            InteractableTextPiece readyText = new InteractableTextPiece("Ready", charSize);
            InteractableTextPiece connectText = new InteractableTextPiece("Connect", charSize);
            InteractableTextPiece changePortText = new InteractableTextPiece("Change port", charSize);
            InteractableTextPiece quitText = new InteractableTextPiece("Quit", charSize);

            connectionText = new TextPiece("Connection not established", 20);
            isReadyText = new TextPiece("Not ready", 20);

            gameTitleLayer1.AddTrait(new CenterTextTrait());
            gameTitleLayer2.AddTrait(new CenterTextTrait());

            readyText.AddTrait(new CenterTextTrait());
            connectText.AddTrait(new CenterTextTrait());
            changePortText.AddTrait(new CenterTextTrait());
            quitText.AddTrait(new CenterTextTrait());

            gameTitleLayer1.AddTrait(new OffsetTrait(new Vector2f(-5, -105)));
            gameTitleLayer2.AddTrait(new OffsetTrait(new Vector2f(5, -95)));

            readyText.AddTrait(new OffsetTrait(new Vector2f(0, -charSize + 100)));
            connectText.AddTrait(new OffsetTrait(new Vector2f(0, 100)));
            changePortText.AddTrait(new OffsetTrait(new Vector2f(0, charSize + 100)));
            quitText.AddTrait(new OffsetTrait(new Vector2f(0, charSize * 2 + 100)));

            int margin = 10;
            connectionText.AddTrait(new OffsetTrait(new Vector2f(margin, GameManager.Game.windowHeight - connectionText.Text.CharacterSize - margin)));
            isReadyText.AddTrait(new OffsetTrait(connectionText.Position - new Vector2f(0, connectionText.Text.CharacterSize)));
            isReadyText.AddTrait(new OffsetTrait(new Vector2f(0, -margin)));

            isReadyText.IsVisible = false;

            readyText.IsEnabled = false;

            readyText.Next = connectText;
            readyText.Previous = quitText;
            connectText.Next = changePortText;
            connectText.Previous = readyText;
            changePortText.Next = quitText;
            changePortText.Previous = connectText;
            quitText.Next = readyText;
            quitText.Previous = changePortText;

            EnableReady += (boolean) =>
            {
                readyText.IsEnabled = boolean;
                isReadyText.IsVisible = boolean;
                connectText.IsEnabled = !boolean;
                changePortText.IsEnabled = !boolean;

                mainMenuUI.FocusedPiece = readyText;
            };

            readyText.Action = () => { IsReady = !IsReady; if (IsReady) Network.Send("imready"); };
            connectText.Action = () => UI = connectUI;
            changePortText.Action = () => UI = changePortUI;
            quitText.Action = () => GameManager.Game.Close();

            mainMenuUI.AddPiece(gameTitleLayer2);
            mainMenuUI.AddPiece(gameTitleLayer1);

            mainMenuUI.AddPiece(readyText);
            mainMenuUI.AddPiece(connectText);
            mainMenuUI.AddPiece(changePortText);
            mainMenuUI.AddPiece(quitText);

            mainMenuUI.AddPiece(connectionText);
            mainMenuUI.AddPiece(isReadyText);

            mainMenuUI.FocusedPiece = connectText;
        }

        private void SetupConnectUI()
        {
            connectUI = new UI();

            InteractableTextPiece addressText = new InteractableTextPiece("IP:PORT", charSize);
            InteractableTextPiece doneText = new InteractableTextPiece("Done", charSize);

            addressText.AddTrait(new WritableTrait(numbers: true, punctuation: true));

            addressText.AddTrait(new CenterTextTrait());
            doneText.AddTrait(new CenterTextTrait());

            doneText.AddTrait(new OffsetTrait(new Vector2f(0, charSize)));

            addressText.Next = doneText;
            addressText.Previous = doneText;
            doneText.Next = addressText;
            doneText.Previous = addressText;

            addressText.Action = () =>
            {
                WritableTrait writableTrait = addressText.GetTrait<WritableTrait>();
                if (writableTrait != null)
                {
                    writableTrait.IsWriting = true;
                    if (addressText.Text.DisplayedString.Equals(writableTrait.DefaultText))
                        addressText.Text.DisplayedString = "";
                }
            };
            doneText.Action = () =>
            {
                string[] splitAddress = addressText.Text.DisplayedString.Split(":");
                if (splitAddress.Length == 2)
                {
                    int port;
                    if (System.Int32.TryParse(splitAddress[1], out port))
                        Network.Connect(splitAddress[0], port,
                            onSent: () => connectionText.Text.DisplayedString = $"Connecting to {addressText.Text.DisplayedString}...",
                            onConnected: () =>
                            {
                                connectionText.Text.DisplayedString = $"Connected to {addressText.Text.DisplayedString}.";
                                EnableReady?.Invoke(true);
                            });
                }
                UI = mainMenuUI;
            };

            connectUI.AddPiece(addressText);
            connectUI.AddPiece(doneText);

            connectUI.FocusedPiece = addressText;
        }

        private void SetupChangePortUI()
        {
            changePortUI = new UI();

            InteractableTextPiece changePortText = new InteractableTextPiece(Network.LocalPort.ToString(), charSize);
            InteractableTextPiece doneText = new InteractableTextPiece("Done", charSize);

            changePortText.AddTrait(new WritableTrait(numbers: true));

            changePortText.AddTrait(new CenterTextTrait());
            doneText.AddTrait(new CenterTextTrait());

            doneText.AddTrait(new OffsetTrait(new Vector2f(0, charSize)));

            changePortText.Next = doneText;
            changePortText.Previous = doneText;
            doneText.Next = changePortText;
            doneText.Previous = changePortText;

            changePortText.Action = () =>
            {
                WritableTrait writableTrait = changePortText.GetTrait<WritableTrait>();
                if (writableTrait != null)
                {
                    writableTrait.IsWriting = true;
                    writableTrait.DefaultText = Network.LocalPort.ToString();
                }
            };
            doneText.Action = () =>
            {
                int port;
                if (Int32.TryParse(changePortText.Text.DisplayedString, out port))
                    Network.LocalPort = port;
                UI = mainMenuUI;
            };

            changePortUI.AddPiece(changePortText);
            changePortUI.AddPiece(doneText);

            changePortUI.FocusedPiece = changePortText;
        }

        protected override void HandleInput()
        {
            UI.HandleInput();

            if (Input.GetKeyPressed(Keyboard.Key.Escape))
            {
                GameManager.Game.Close();
            }
        }

        public override void Update()
        {
            base.Update();

            HandleInput();
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(background);
            target.Draw(UI);
        }
    }
}