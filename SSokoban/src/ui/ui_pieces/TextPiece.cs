using SFML.Graphics;
using SFML.System;

namespace SSokoban.Interface
{
    public class TextPiece : UIPiece
    {
        public Text Text { get; set; }

        private bool isEnabled = true;
        public override bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; Text.FillColor = isEnabled ? notFocusedColor : disabledColor; }
        }

        public override Vector2f Position { get { return Text.Position; } set { Text.Position = value; } }

        public static Color notFocusedColor { get; private set; } = new Color(206, 163, 173);//new Color(150, 150, 150);
        public static Color focusedColor { get; private set; } = new Color(230, 201, 195);//new Color(230, 230, 230);
        public static Color disabledColor { get; private set; } = new Color(141, 84, 137);//new Color(100, 100, 100);

        public TextPiece(string str, uint size)
        {
            Text = new Text(str, UI.Font, size);
            Text.FillColor = notFocusedColor;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Text);
        }
    }
}