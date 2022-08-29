using System.Collections.Generic;

using SFML.Graphics;
using SFML.System;

namespace SSokoban.Interface
{
    public abstract class UIPiece : Drawable
    {
        public UI ui;

        public abstract bool IsEnabled { get; set; }
        public bool IsVisible { get; set; } = true;

        public abstract Vector2f Position { get; set; }

        protected List<UITrait> traits = new List<UITrait>();

        public void AddTrait(UITrait trait)
        {
            traits.Add(trait);
            trait.Piece = this;
            ApplicableTrait applicableTrait = trait as ApplicableTrait;
            applicableTrait?.Apply();
        }

        public T GetTrait<T>() where T : UITrait
        {
            foreach (UITrait trait in traits)
                if (trait.GetType().Equals(typeof(T)))
                    return (T) trait;

            return null;
        }

        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}