using SFML.System;
using SFML.Window;
using SFML.Graphics;

using SSokoban.EntitiesAndComponents;
using SSokoban.Core;
using SSokoban.MapsAndSections;
using SSokoban.Interface;

namespace SSokoban.GameStates
{
    public class PlayingState : GameState
    {
        public Map Map { get; set; }

        private UI ui;

        public PlayingState(Map map)
        {
            Map = map;
            PlayerController.Player = map.Player;
            ScaleAndPositionSprites();
            MovementTransactionSystem.Map = Map;

            ui = new UI();
            TextPiece textPiece = new TextPiece("Press 'SPACE' to RESTART", 20);
            textPiece.AddTrait(new OffsetTrait(new Vector2f(10, 5)));
            ui.AddPiece(textPiece);
        }

        private void ScaleAndPositionSprites()
        {
            foreach (Entity entity in Map.Entities)
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
            PlayerController.HandleInput();

            if (Input.GetKeyPressed(Keyboard.Key.Space))
            {
                GameManager.Restart();
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
            CollisionSystem.CheckCollisions(Map);
            HandleInput();
            UpdateEntities();
            GameManager.CheckIfAllRocksAreInPlaces();
        }

        private void UpdateEntities()
        {
            foreach (Entity entity in Map.Entities)
                entity.UpdateComponents();
        }

        public override void Draw(RenderTarget target)
        {
            foreach (Entity entity in Map.Entities)
            {
                SpriteComponent spriteComponent = entity.GetComponent<SpriteComponent>();

                if (spriteComponent == null)
                    continue;

                target.Draw(spriteComponent);
            }

            target.Draw(ui);
        }
    }
}