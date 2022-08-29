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
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                GameManager.GameState = new MainMenuState();
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                GameManager.Game.Close();
            }
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(ui);
        }
    }
}