using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PInfo : MonoBehaviour {

    private MGameBoard model;

    [SerializeField]
    private Text currentAnswerText;

    private void Awake() {
    }

    private void Start() {
        model = MGameBoard.getInstance();

        model.currentAnswer
            .ObserveCountChanged()
            .Subscribe(Count => {
                currentAnswerText.text = "";
                for(int i = 0; i < Count; ++i) {
                    currentAnswerText.text += model.currentAnswer[i].ToString();
                    currentAnswerText.text += " ";
                }
            });
    }

}
