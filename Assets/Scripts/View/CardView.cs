using Domain;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class CardView : MonoBehaviour
    {
        public Animator animator;
        public Image baseImage;
        public Image iconImage;
        
        private static readonly int IsSelected = Animator.StringToHash("IsSelected");

        public void UpdateView(in CardData data)
        {
            this.animator.SetBool(IsSelected, data.isSelected);
            this.iconImage.sprite = data.iconImage;
            this.iconImage.color = data.iconColor;
            this.baseImage.color = data.baseColor;
        }
    }
}
