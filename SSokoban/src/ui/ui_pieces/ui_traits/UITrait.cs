using System;

namespace SSokoban.Interface
{
    public abstract class UITrait
    {
        private UIPiece piece;
        public UIPiece Piece { get { return piece; } set { piece = value; OnPieceSet?.Invoke(); } } 

        public Action OnPieceSet;
    }
}