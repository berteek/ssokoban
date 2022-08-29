using System.Collections.Generic;

using SFML.Graphics;
using SFML.Window;
using SSokoban.Core;
using SSokoban.Properties;

namespace SSokoban.Interface
{
    public class UI : Drawable
    {
        public static Font Font { get; private set; } = new Font(Resources.Pixel_NES);

        private List<UIPiece> pieces = new List<UIPiece>();

        private InteractableTextPiece focusedPiece;
        public InteractableTextPiece FocusedPiece
        {
            get { return focusedPiece; }
            set { focusedPiece?.OnUnfocused(); focusedPiece = value; focusedPiece.OnFocused(); }
        }

        public void AddPiece(UIPiece piece)
        {
            pieces.Add(piece);
            piece.ui = this;
        }

        public T GetPiece<T>() where T : UIPiece
        {
            foreach (UIPiece piece in pieces)
                if (piece.GetType().Equals(typeof(T)))
                    return (T)piece;

            return null;
        }

        public void HandleInput()
        {
            if (FocusedPiece != null)
            {
                WritableTrait writableTrait = FocusedPiece.GetTrait<WritableTrait>();

                if (writableTrait != null)
                {
                    if (writableTrait.IsWriting)
                    {
                        writableTrait.HandleInput();
                        return;
                    }
                }

                if (Input.GetKeyPressed(Keyboard.Key.Up) || Input.GetKeyPressed(Keyboard.Key.W))
                    FocusPrevious();
                else if (Input.GetKeyPressed(Keyboard.Key.Down) || Input.GetKeyPressed(Keyboard.Key.S))
                    FocusNext();
                else if (Input.GetKeyPressed(Keyboard.Key.Enter) || Input.GetKeyPressed(Keyboard.Key.Space))
                    FocusedPiece.Action?.Invoke();
            }
        }

        private void FocusNext()
        {
            do
            {
                FocusedPiece = FocusedPiece.Next;
            } while (!FocusedPiece.IsEnabled);
        }

        private void FocusPrevious()
        {
            do
            {
                FocusedPiece = FocusedPiece.Previous;
            } while (!FocusedPiece.IsEnabled);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (UIPiece piece in pieces)
            {
                if (piece.IsVisible)
                    target.Draw(piece);
            }
        }
    }
}