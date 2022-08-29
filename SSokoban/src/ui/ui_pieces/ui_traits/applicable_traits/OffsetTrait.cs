using SFML.System;

namespace SSokoban.Interface
{
    public class OffsetTrait : ApplicableTrait
    {
        private Vector2f offset;

        public OffsetTrait(Vector2f offset)
        {
            this.offset = offset;
        }

        public override void Apply()
        {
            Piece.Position += offset;
        }
    }
}