using SFML.Graphics;
using SFML.System;

using SSokoban.Core;

using System;
using System.Numerics;

namespace SSokoban.EntitiesAndComponents
{
    public class SpriteComponent : Component, Drawable
    {
        private Texture texture;
        private Sprite sprite;

        public Sprite Sprite { get { return sprite; } set { sprite = value; } }

        public Vector2f Scale { get { return sprite.Scale; } set { sprite.Scale = value; } }
        public Vector2f Position { get { return sprite.Position; } set { sprite.Position = value; } }

        public static int SPRITE_SIZE { get; set; } = 32;

        private float interpolationWeight = 12.5f;
        private float epsilon = 0.001f;
        private float snapValue = 3.5f; //7.5f

        public SpriteComponent(Texture texture)
        {
            this.texture = texture;
            this.texture.Smooth = false;
            sprite = new Sprite(texture);
        }

        public override void Update()
        {
            if (Math.Abs(entity.Position.X * GameManager.UNIT - sprite.Position.X) < epsilon
                && Math.Abs(entity.Position.Y * GameManager.UNIT - sprite.Position.Y) < epsilon)
                return;

            TurnSprite();
            MoveSprite();
        }

        private void TurnSprite()
        {
            Vector2f direction = new Vector2f(entity.Position.X * GameManager.UNIT - sprite.Position.X, entity.Position.Y * GameManager.UNIT - sprite.Position.Y);

            if (Math.Sign(direction.X) == -1)
            {
                sprite.Scale = new Vector2f(-Math.Abs(sprite.Scale.X), sprite.Scale.Y);
                sprite.Origin = new Vector2f(sprite.GetGlobalBounds().Width / Math.Abs(sprite.Scale.X), 0.0f);
            }
            else if (Math.Sign(direction.X) == 1)
            {
                sprite.Scale = new Vector2f(Math.Abs(sprite.Scale.X), sprite.Scale.Y);
                sprite.Origin = new Vector2f(0.0f, 0.0f);
            }
        }

        private void MoveSprite()
        {
            Vector2 interpolatedVector = Vector2.Lerp
            (
                new Vector2(sprite.Position.X, sprite.Position.Y),
                new Vector2(entity.Position.X * GameManager.UNIT, entity.Position.Y * GameManager.UNIT),
                interpolationWeight * GameManager.dt
            );

            Vector2f transformVector = new Vector2f(interpolatedVector.X, interpolatedVector.Y);

            if (Math.Abs(entity.Position.X * GameManager.UNIT - sprite.Position.X) < snapValue
                && Math.Abs(entity.Position.Y * GameManager.UNIT - sprite.Position.Y) < snapValue)
                transformVector = (Vector2f)entity.Position * GameManager.UNIT;

            sprite.Position = transformVector;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprite);
        }
    }
}