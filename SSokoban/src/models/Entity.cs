using System.Collections.Generic;

using SFML.System;

namespace SSokoban.EntitiesAndComponents
{
    public sealed class Entity
    {
        public Vector2i Position { get; set; }
        public int ZIndex { get; set; } = 1;

        public string Name { get; set; } = "";
        public string Tag { get; set; } = "";

        private List<Component> components = new List<Component>();

        public Entity(Vector2i position)
        {
            Position = position;
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
            component.entity = this;
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in components)
                if (component.GetType().Equals(typeof(T)))
                    return (T)component;

            return null;
        }

        public void UpdateComponents()
        {
            foreach (Component component in components)
                component.Update();
        }
    }
}