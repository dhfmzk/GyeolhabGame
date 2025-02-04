using System;
using Component.Interface;
using GameSystem;
using TMPro;
using UnityEngine;

namespace Component
{
    [Serializable]
    public class InfoComponent : MonoBehaviour, IAwakableComponent, IUpdatableComponent
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI comboText;

        private int _prevScore;
        private int _prevCombo;

        private const string FormatScore = "{0} Point";
        private const string FormatCombo = "{0} Combo";

        public void ComponentAwake()
        {
            this.scoreText.text = string.Format(FormatScore, GameLoop.I.GameModel.Score.ToString());
            this.comboText.text = string.Format(FormatCombo, GameLoop.I.GameModel.Combo.ToString());
        }

        public void ComponentUpdate(TimeSpan deltaTime)
        {
            if (GameLoop.I.GameModel.Score != this._prevScore)
            {
                this.scoreText.text = string.Format(FormatScore, GameLoop.I.GameModel.Score.ToString());
            }

            if (GameLoop.I.GameModel.Combo != this._prevCombo)
            {
                this.comboText.text = string.Format(FormatCombo, GameLoop.I.GameModel.Combo.ToString());
            }
        }
    }
}
