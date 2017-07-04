using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class PTimer : MonoBehaviour {

    private MGameBoard model;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private Slider timerBar;

    private void Awake() {
    }

    private void Start() {
        model = MGameBoard.getInstance();

        model.timerReactiveProperty
            .Subscribe(x => {
                TimeSpan temp = TimeSpan.FromSeconds(x);
                timerText.text = string.Format("{0:D2}:{1:D2}",temp.Minutes, temp.Seconds);
                timerBar.value = x / (float)model.maxTime;
            });
    }
}
