using SFML.Graphics;
using SFML.Window;

using SSokoban.Interface;
using SSokoban.Core;

namespace SSokoban.GameStates
{
    public class WinState : GameState
    {
        private UI ui;

        public WinState()
        {
            ui = new UI();
            TextPiece textPiece = new TextPiece("YOU WIN!", 50);
            textPiece.AddTrait(new CenterTextTrait());
            ui.AddPiece(textPiece);
        }

        protected override void HandleInput()
        {
            if (Input.GetKeyPressed(Keyboard.Key.Space))
            {
                GameManager.GameState = new MainMenuState();
            }

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
            target.Draw(ui);
        }
    }
}