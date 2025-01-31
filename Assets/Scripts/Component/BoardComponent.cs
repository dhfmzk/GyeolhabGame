using System;
using Component.Interface;
using GameSystem;
using R3;
using UnityEngine;
using View;

namespace Component
{
    public class BoardComponent : MonoBehaviour, IAwakableComponent, IUpdatableComponent
    {
        public BoardView boardView;

        public void ComponentAwake()
        {
            this.boardView.OnClickAsObservable()
                .Where(_ => GameLoop.I.GameModel.IsGamePlaying)
                .Subscribe(this.OnClickCard)
                .AddTo(this);
        }

        public void ComponentUpdate(TimeSpan deltaTime)
        {
        }

        public void NewGame()
        {
            // // Answer Calc logic
            // var currentAnswerStream = localGameModel.currentAnswer
            //                             .ObserveCountChanged()
            //                             .Where(e => e == 3);
            //
            // currentAnswerStream
            //     .Where(e => IsCorrectAnswer())
            //     .Subscribe(e => {
            //         localGameModel.score.Value += 2;
            //         localGameModel.combo.Value++;
            //         localGameModel.currentAnswer.Clear();
            //     });
            //
            // currentAnswerStream
            //     .Where(e => !IsCorrectAnswer())
            //     .Subscribe(e => 
            //     {
            //         localGameModel.score.Value -= 2;
            //         localGameModel.combo.Value = 0;
            //         localGameModel.currentAnswer.Clear();
            //     });
        }
    
        private void OnClickCard(int n)
        {
            if (!GameLoop.I.GameModel.TryPickCard(n))
            {
                // TODO : 알람
            }

            if (GameLoop.I.GameModel.IsCompletePicked)
            {
                return;
            }

            GameLoop.I.GameModel.TrySubmitAnswer();
        }
    }
}
