using System;

using SFML.Graphics;

namespace SSokoban.Interface
{
    public class InteractableTextPiece : TextPiece
    {
        public Action Action { get; set; }

        public InteractableTextPiece Next { get; set; }
        public InteractableTextPiece Previous { get; set; }
        
        public InteractableTextPiece(string str, uint size, Action action = null) : base(str, size)
        {
            Action = action;
            Text.FillColor = notFocusedColor;
        }

        public void OnFocused()
        {
            Text.FillColor = focusedColor;
        }

        public void OnUnfocused()
        {
            Text.FillColor = IsEnabled ? notFocusedColor : disabledColor;
        }
    }
}