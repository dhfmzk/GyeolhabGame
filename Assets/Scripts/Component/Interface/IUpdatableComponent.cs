using System;
using UnityEngine;

namespace Component.Interface
{
    public interface IUpdatableComponent
    {
        public void ComponentUpdate(TimeSpan deltaTime);
    }
}