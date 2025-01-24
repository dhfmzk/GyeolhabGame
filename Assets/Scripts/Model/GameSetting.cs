using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class GameSetting
    {
        [Header("Icon Sprite Setting")]
        public Sprite icon1;
        public Sprite icon2;
        public Sprite icon3;
        
        [Space]
        [Header("Icon Color Setting")]
        public Color colorIcon1;
        public Color colorIcon2;
        public Color colorIcon3;

        [Space]
        [Header("Base Color Setting")]
        public Color colorBase1;
        public Color colorBase2;
        public Color colorBase3;
       
    }
}