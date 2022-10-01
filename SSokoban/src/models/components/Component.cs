namespace SSokoban.EntitiesAndComponents
{
    public abstract class Component
    {
        public Entity entity;

        public virtual void Update()
        {}
    }
}