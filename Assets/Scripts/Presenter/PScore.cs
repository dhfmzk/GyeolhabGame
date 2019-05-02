using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PScore : MonoBehaviour {

    private MGameBoard model;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text comboText;

    private void Awake() {
    }

    private void Start() {
        model = MGameBoard.getInstance();

        model.score.AsObservable()
            .Subscribe(Score => {
                scoreText.text = Score.ToString() + " 점";
            });

        model.combo.AsObservable()
            .Subscribe(Combo => {
                comboText.text = Combo.ToString() + "콤보!";
            });

    }
}
