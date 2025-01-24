using System;
using UnityEngine;

namespace Domain
{
    [Serializable]
    public record CardData
    {
        public bool isSelected;
        public Sprite iconImage; 
        public Color iconColor;
        public Color baseColor;
    }
}