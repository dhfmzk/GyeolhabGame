using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ScoreBoardView : MonoBehaviour {

    public Text scoreText;
    public Text comboText;

    public void SetupScore(int _score) {

        scoreText.text = $"{_score} Point";
    }

    public void SetupCombo(int _combo) {

        comboText.text = $"{_combo} Combo";
    }

    public void UpdateView(int _score, int _combo) {

        SetupScore(_score);
        SetupCombo(_combo);
    }
}
