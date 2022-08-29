using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

using SSokoban.Interface;

namespace SSokoban.GameStates
{
    public class MapTitleState : GameState
    {
        private Clock clock;
        private int duration;

        private UI ui;
        private TextPiece textPiece;

        public Action OnMapTitleEnded { get; set; }

        public MapTitleState(string title, Action OnMapTitleEnded, int duration = 3)
        {
            clock = new Clock();
            this.duration = duration;
            this.OnMapTitleEnded = OnMapTitleEnded;

            ui = new UI();
            textPiece = new TextPiece(title, 50);
            textPiece.AddTrait(new CenterTextTrait());
            textPiece.Text.FillColor = TextPiece.focusedColor;

            ui.AddPiece(textPiece);
        }

        public override void Update()
        {
            if (clock.ElapsedTime.AsSeconds() >= duration)
            {
                OnMapTitleEnded();
            }
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(ui);
        }
    }
}