using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ScoreView : MonoBehaviour
    {
        public Text scoreText;
        public Text comboText;

        public void SetupScore(int score)
        {

            this.scoreText.text = $"{score} Point";
        }

        public void SetupCombo(int combo)
        {

            this.comboText.text = $"{combo} Combo";
        }

        public void UpdateView(int score, int combo)
        {

            this.SetupScore(score);
            this.SetupCombo(combo);
        }
    }
}
