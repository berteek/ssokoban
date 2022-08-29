using SFML.Graphics;

using SSokoban.Core;

namespace SSokoban.Interface
{
    public class CenterTextTrait : ApplicableTrait
    {
        public override void Apply()
        {
            TextPiece textPiece = Piece as TextPiece;
            FloatRect rectangle = textPiece.Text.GetLocalBounds();
            textPiece.Text.Origin = new SFML.System.Vector2f(rectangle.Left + rectangle.Width/2.0f, rectangle.Top + rectangle.Height/2.0f);
            textPiece.Text.Position = new SFML.System.Vector2f(GameManager.Game.windowWidth/2.0f, GameManager.Game.windowHeight/2.0f);
        }
    }
}