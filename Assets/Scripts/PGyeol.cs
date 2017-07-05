using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PGyeol : MonoBehaviour {

    private MGameBoard model;

    [SerializeField]
    private Button gyeolButton;

    private void Awake() {
    }

    private void Start() {
        model = MGameBoard.getInstance();

        gyeolButton.OnClickAsObservable()
            .Subscribe(_ => {
                if(model.answers.Count == 0) {
                    Debug.Log("정답");
                    model.score.Value += (model.combo.Value + 1) * 3;
                }
            });
    }
}
