using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ScoreBoardView : MonoBehaviour {

        public Text scoreText;
        public Text comboText;

        public void SetupScore(int _score)
        {

            this.scoreText.text = $"{_score} Point";
        }

        public void SetupCombo(int _combo)
        {

            this.comboText.text = $"{_combo} Combo";
        }

        public void UpdateView(int _score, int _combo)
        {

            this.SetupScore(_score);
            this.SetupCombo(_combo);
        }
    }
}
