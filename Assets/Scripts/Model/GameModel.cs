using System;
using GameSystem;
using UnityEngine;
using R3;
using UnityEngine.Serialization;

namespace Model
{
    [Serializable]
    public class GameModel : IGameModel
    {
        // Settings
        public float resetTime = 3000.0f;

        // State
        public GameState GameState { get; private set; }
        public PlayingState PlayingState { get; private set; }
        
        // Timer
        public float RemainTurnTime { get; private set; }

        public GameModel()
        {
            this.GameState = GameState.Main;
            this.PlayingState = PlayingState.None;
        }

        public bool IsGamePlaying => this.GameState == GameState.Playing;
        public void StartGame()
        {
            this.GameState = GameState.Playing;
        }

        private void Start()
        {
            // Timmer Model
            // Observable.Interval(TimeSpan.FromMilliseconds(1))
            //     .Where(_ => this.currentCards.Count != 0)
            //     .Subscribe(_ => {
            //         if(this.timerReactiveProperty.Value > 0) {
            //             this.timerReactiveProperty.Value--;
            //         }
            //         else {
            //             this.combo.Value = 0;
            //             this.score.Value--;
            //             this.timerReactiveProperty.Value = MAX_TIME;
            //         }
            //     })
            //     .AddTo(this);
        }

        public void DecreaseTime(float value)
        {
            this.RemainTurnTime -= value;
        }

        public void ResetTime()
        {
            this.RemainTurnTime = this.resetTime;
        }
    }
}
