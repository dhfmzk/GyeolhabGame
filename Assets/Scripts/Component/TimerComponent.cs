using System;
using Component.Interface;
using Domain;
using GameSystem;
using UnityEngine;
using View;

namespace Component
{
    [Serializable]
    public class TimerComponent : MonoBehaviour, IStartableComponent, IUpdatableComponent
    {
        public TimerView timerView;

        public void ComponentStart()
        {
            this.timerView.UpdateView(TimerData.Default);
        }
        
        public void ComponentUpdate(TimeSpan deltaTime)
        {
            if (!GameLoop.I.GameModel.IsGamePlaying)
            {
                GameLoop.I.GameModel.ResetTime();
                return;
            }
            
            GameLoop.I.GameModel.DecreaseTime(deltaTime);

            if (GameLoop.I.GameModel.RemainTurnTime <= 0.0d)
            {
                GameLoop.I.GameModel.ResetTime();
            }
            
            this.timerView.UpdateView(new TimerData
            {
                remainTime = GameLoop.I.GameModel.RemainTurnTime,
            });
        }
    }
}
