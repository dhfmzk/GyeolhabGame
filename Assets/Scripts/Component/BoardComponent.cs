using System;
using System.Collections.Generic;
using Component.Interface;
using Domain;
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
            for (var i = 0; i < GameLoop.I.GameModel.DeckSize; i++)
            {
                this.boardView.UpdateView(
                    index: i,
                    data: new CardData
                    {
                        isSelected = GameLoop.I.GameModel.IsPicked(i),
                        iconImage = GameLoop.I.GameSetting.GetIconImage(i),
                        iconColor = GameLoop.I.GameSetting.GetIconColor(i),
                        baseColor = GameLoop.I.GameSetting.GetBaseColor(i),
                    });
            }
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
