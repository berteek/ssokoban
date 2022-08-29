using SFML.Window;
using SSokoban.Core;

namespace SSokoban.Interface
{
    public class WritableTrait : UITrait
    {
        public bool IsWriting { get; set; } = false;

        public string DefaultText { get; set; } = "";

        private bool numbers;
        private bool punctuation;

        public WritableTrait(bool numbers = false, bool punctuation = false)
        {
            this.numbers = numbers;
            this.punctuation = punctuation;

            OnPieceSet += () =>
            {
                TextPiece textPiece = Piece as TextPiece;
                if (textPiece != null)
                    DefaultText = textPiece.Text.DisplayedString;
            };
        }

        public void HandleInput()
        {
            TextPiece textPiece = Piece as TextPiece;

            if (textPiece == null)
                return;

            if (numbers)
            {
                if (Input.GetKeyPressed(Keyboard.Key.Num0))
                    textPiece.Text.DisplayedString += "0";
                else if (Input.GetKeyPressed(Keyboard.Key.Num1))
                    textPiece.Text.DisplayedString += "1";
                else if (Input.GetKeyPressed(Keyboard.Key.Num2))
                    textPiece.Text.DisplayedString += "2";
                else if (Input.GetKeyPressed(Keyboard.Key.Num3))
                    textPiece.Text.DisplayedString += "3";
                else if (Input.GetKeyPressed(Keyboard.Key.Num4))
                    textPiece.Text.DisplayedString += "4";
                else if (Input.GetKeyPressed(Keyboard.Key.Num5))
                    textPiece.Text.DisplayedString += "5";
                else if (Input.GetKeyPressed(Keyboard.Key.Num6))
                    textPiece.Text.DisplayedString += "6";
                else if (Input.GetKeyPressed(Keyboard.Key.Num7))
                    textPiece.Text.DisplayedString += "7";
                else if (Input.GetKeyPressed(Keyboard.Key.Num8))
                    textPiece.Text.DisplayedString += "8";
                else if (Input.GetKeyPressed(Keyboard.Key.Num9))
                    textPiece.Text.DisplayedString += "9";
            }

            if (punctuation)
            {
                if (Input.GetKeyPressed(Keyboard.Key.Period))
                {
                    textPiece.Text.DisplayedString += ".";
                }
                else if (Input.GetKeyPressed(Keyboard.Key.Semicolon) && Input.GetKeyPressed(Keyboard.Key.LShift))
                {
                    textPiece.Text.DisplayedString += ":";
                }
            }

            if (Input.GetKeyPressed(Keyboard.Key.Backspace) && textPiece.Text.DisplayedString.Length > 0)
            {
                textPiece.Text.DisplayedString = textPiece.Text.DisplayedString.Remove(textPiece.Text.DisplayedString.Length - 1);
            }

            if (Input.GetKeyPressed(Keyboard.Key.Enter) || Input.GetKeyPressed(Keyboard.Key.Space))
            {
                IsWriting = false;
                if (textPiece.Text.DisplayedString.Equals(""))
                    textPiece.Text.DisplayedString = DefaultText;
            }

            CenterTextPiece(textPiece);
        }

        private void CenterTextPiece(TextPiece textPiece)
        {
            CenterTextTrait centerTextTrait = textPiece.GetTrait<CenterTextTrait>();
            centerTextTrait?.Apply();
        }
    }
}