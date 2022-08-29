using SFML.Window;

namespace SSokoban.EntityStates
{
    public abstract class PlayerState
    {
        public virtual void HandleInput() { }
    }
}