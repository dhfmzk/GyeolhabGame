using System;
using Domain;
using GameSystem;
using R3;
using UnityEngine;
using UnityEngine.UI;
using R3.Triggers;
using UnityEngine.EventSystems;

namespace View
{
    public class CardView : MonoBehaviour
    {
        public Animator animator;
        public Image baseImage;
        public Image iconImage;
        public Button button;
        
        private static readonly int IsSelected = Animator.StringToHash("IsSelected");

        public void Awake()
        {
        }

        public Observable<PointerEventData> OnClickAsObservable()
        {
            return this.button.OnPointerClickAsObservable()
                .Where(_ => GameLoop.I.GameModel.IsGamePlaying);
        }

        public void UpdateView(in CardData data)
        {
            this.animator.SetBool(IsSelected, data.isSelected);
            this.iconImage.sprite = data.iconImage;
            this.iconImage.color = data.iconColor;
            this.baseImage.color = data.baseColor;
        }
    }
}
