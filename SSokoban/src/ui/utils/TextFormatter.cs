using SFML.Graphics;

using SSokoban.Core;

namespace SSokoban.Interface
{
    public static class TextFormatter
    {
        public static void CenterText(Text text)
        {
            FloatRect rectangle = text.GetLocalBounds();
            text.Origin = new SFML.System.Vector2f(rectangle.Left + rectangle.Width/2.0f, rectangle.Top + rectangle.Height/2.0f);
            text.Position = new SFML.System.Vector2f(GameManager.Game.windowWidth/2.0f, GameManager.Game.windowHeight/2.0f);
        }

        public static void CenterTextHorizontally(Text text)
        {
            FloatRect rectangle = text.GetLocalBounds();
            text.Origin = new SFML.System.Vector2f(rectangle.Left + rectangle.Width/2.0f, text.Origin.Y);
            text.Position = new SFML.System.Vector2f(GameManager.Game.windowWidth/2.0f, text.Position.Y);
        }

        public static void BottomLeftCorner(Text text)
        {
            FloatRect rectangle = text.GetLocalBounds();
            int margin = 10;
            text.Position = new SFML.System.Vector2f(margin, GameManager.Game.windowHeight - rectangle.Height - rectangle.Top - margin);
        }
    }
}