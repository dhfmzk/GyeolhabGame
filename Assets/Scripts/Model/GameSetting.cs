using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class GameSetting
    {
        [Header("Icon Sprite Setting")] 
        public Sprite[] iconImages;
        
        [Space]
        [Header("Icon Color Setting")]
        public Color[] iconColors;

        [Space]
        [Header("Base Color Setting")]
        public Color[] baseColors;

        public Sprite GetIconImage(int value)
        {
            var index = value / 9;
            
            return this.iconImages[index];
        }
        
        public Color GetIconColor(int value)
        {
            var index = value % 9 / 3;
            
            return this.iconColors[index];
        }
        
        public Color GetBaseColor(int value)
        {
            var index = value % 3;
            
            return this.baseColors[index];
        }
    }
}