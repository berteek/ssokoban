using SFML.Graphics;
using SFML.Window;

using SSokoban.Interface;
using SSokoban.Core;
using SSokoban.Utils;

namespace SSokoban.GameStates
{
    public class LoseState : GameState
    {
        private UI ui;

        public LoseState()
        {
            ui = new UI();
            TextPiece textPiece = new TextPiece("YOU LOSE!", 100);
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