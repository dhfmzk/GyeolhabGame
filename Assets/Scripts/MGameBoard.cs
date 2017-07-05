using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MGameBoard : MonoBehaviour {

    // Singleton Instance
    private static MGameBoard _instance = null;
    public static MGameBoard getInstance() {
        return _instance;
    }

    // Model for Timer
    private static int _maxTime = 3000;
    public int maxTime;
    public ReactiveProperty<int> timerReactiveProperty;

    // Model for GameBoard
    public Sprite[] _cardDeck = new Sprite[27];
    public ReactiveCollection<Sprite> currentCards;
    public ReactiveCollection<int[]> answers;
    public ReactiveCollection<int> currentAnswer;

    // Model for Score
    public ReactiveProperty<int> score;
    public ReactiveProperty<int> combo;

    private void Awake() {
        // Set Timer Model
        maxTime = _maxTime;
        timerReactiveProperty = new IntReactiveProperty(_maxTime);

        // Set GameBoard Model
        for(int i = 0; i < 27; ++i) {
            _cardDeck[i] = Resources.Load<Sprite>("Cards/card_" + i.ToString());
        }
        currentCards = new ReactiveCollection<Sprite>();
        answers = new ReactiveCollection<int[]>();
        currentAnswer = new ReactiveCollection<int>();

        // Set Score Model
        score = new IntReactiveProperty(0);
        combo = new IntReactiveProperty(0);

        if(_instance == null) {
            _instance = this;
        }
        else if(_instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        // Timmer Model
        Observable.Interval(TimeSpan.FromMilliseconds(1))
            .Subscribe(_ => {
                if(timerReactiveProperty.Value > 0) {
                    timerReactiveProperty.Value--;
                }
                else {
                    combo.Value = 0;
                    score.Value--;
                    timerReactiveProperty.Value = _maxTime;
                }
            })
            .AddTo(this);
    }

    public void ResetGame() {
        timerReactiveProperty.Value = _maxTime;
    }

}
