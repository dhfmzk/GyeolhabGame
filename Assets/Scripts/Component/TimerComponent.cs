using System;
using Component.Interface;
using GameSystem;
using R3;
using UnityEngine;
using View;

namespace Component
{
    [Serializable]
    public class TimerComponent : MonoBehaviour, IUpdatableComponent
    {
        public TimerView timerView;

        public void ComponentUpdate(TimeSpan deltaTime)
        {
            if (!GameLoop.I.GameModel.IsGamePlaying)
            {
                GameLoop.I.GameModel.ResetTime();
                return;
            }
            
            GameLoop.I.GameModel.DecreaseTime(deltaTime);

            if (GameLoop.I.GameModel.RemainTurnTime <= TimeSpan.FromSeconds(0.0f))
            {
                GameLoop.I.GameModel.ResetTime();
            }
        }
    }
}
