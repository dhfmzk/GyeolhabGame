using System;
using Domain;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class CardView : MonoBehaviour
    {
        public Animator animator;
        public Image baseImage;
        public Image iconImage;
        public Button button;
        
        private static readonly int IsSelected = Animator.StringToHash("IsSelected");

        public Observable<Unit> OnClickAsObservable()
        {
            return this.button.OnClickAsObservable();
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
