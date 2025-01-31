using System;

namespace Component.Interface
{
    public interface IUpdatableComponent
    {
        public void ComponentUpdate(TimeSpan deltaTime);
    }
}