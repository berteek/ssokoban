using SFML.Graphics;

namespace SSokoban.GameStates
{
    public abstract class GameState
    {
        protected virtual void HandleInput() { }
        public virtual void Draw(RenderTarget target) { }
        public virtual void Update() { }
    }
}