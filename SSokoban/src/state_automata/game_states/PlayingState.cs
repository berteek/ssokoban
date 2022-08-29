using SFML.Window;
using SFML.Graphics;

using SSokoban.EntitiesAndComponents;
using SSokoban.Core;
using SSokoban.MapsAndSections;
using SSokoban.Utils;
using SFML.System;

namespace SSokoban.GameStates
{
    public class PlayingState : GameState
    {
        public Section Section { get; set; }

        public PlayingState(Section section)
        {
            Section = section;
            PlayerController.Player = section.Player;
            ScaleAndPositionSprites();
            MovementTransactionSystem.Section = Section;
        }

        private void ScaleAndPositionSprites()
        {
            foreach (Entity entity in Section.Entities)
            {
                SpriteComponent spriteComponent = entity.GetComponent<SpriteComponent>();
                if (spriteComponent != null)
                {
                    spriteComponent.Scale = new Vector2f(1, 1) * (GameManager.UNIT / SpriteComponent.SPRITE_SIZE);
                    spriteComponent.Position = (Vector2f)entity.Position * GameManager.UNIT;
                }
            }
        }

        protected override void HandleInput()
        {
            PlayerController.PlayerState.HandleInput();

            if (Input.GetKeyPressed(Keyboard.Key.R) && Input.GetKeyPressed(Keyboard.Key.LShift))
            {
                GameManager.Restart();
                Network.Send("restart");
            }

            if (Input.GetKeyPressed(Keyboard.Key.Escape))
            {
                GameManager.Game.Close();
            }
        }

        public override void Update()
        {
            base.Update();

            MovementTransactionSystem.Process();
            CollisionSystem.CheckCollisions(Section);
            HandleInput();
            UpdateEntities();
        }

        private void UpdateEntities()
        {
            foreach (Entity entity in Section.Entities)
                entity.UpdateComponents();
        }

        public override void Draw(RenderTarget target)
        {
            foreach (Entity entity in Section.Entities)
            {
                SpriteComponent spriteComponent = entity.GetComponent<SpriteComponent>();

                if (spriteComponent == null)
                    continue;

                target.Draw(spriteComponent);
            }
        }
    }
}