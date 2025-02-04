using System;
using Component.Interface;
using Domain;
using GameSystem;
using Model.Interface;
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
            for (var i = 0; i < GameLoop.I.GameModel.DeckSize; i++)
            {
                var cardNumber = GameLoop.I.GameModel.DeckList[i];
                this.boardView.UpdateView(
                    index: i,
                    data: new CardData
                    {
                        isSelected = GameLoop.I.GameModel.IsPicked(cardNumber),
                        iconImage = GameLoop.I.GameSetting.GetIconImage(cardNumber),
                        iconColor = GameLoop.I.GameSetting.GetIconColor(cardNumber),
                        baseColor = GameLoop.I.GameSetting.GetBaseColor(cardNumber),
                    });
            }
        }
    
        private void OnClickCard(int value)
        {
            var result = GameLoop.I.GameModel.ToggleCard(value);
            
            Debug.Log($"[{typeof(BoardComponent)}|{result}] Picked Answer : {GameLoop.I.GameModel.GetPicked()} | Answer : {GameLoop.I.GameModel.GetAnswers()}");

            if (result != ToggleResult.ToggleOn)
            {
                return;
            }
            
            if (GameLoop.I.GameModel.IsPickCompleted)
            {
                GameLoop.I.GameModel.SubmitPicked();
            }
        }
    }
}
