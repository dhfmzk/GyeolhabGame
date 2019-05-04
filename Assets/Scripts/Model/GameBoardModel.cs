using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameBoardModel : MonoBehaviour {

    // Model for Timer
    public static int _maxTime = 3000;
    public int maxTime;
    public ReactiveProperty<int> timerReactiveProperty;

    // Model for GameBoard
    public Sprite[] _cardDeck = new Sprite[27];
    public ReactiveCollection<Sprite> currentCards;
    public ReactiveCollection<int[]> answers;
    public ReactiveCollection<int> currentAnswer;

    // Model for Score
    public IntReactiveProperty score;
    public IntReactiveProperty combo;
    public IntReactiveProperty maxCombo;

    private void Awake() {
        // Set Timer Model
        maxTime = _maxTime;
        timerReactiveProperty = new IntReactiveProperty(_maxTime);

        // Set GameBoard Model
        for(int i = 0; i < 27; ++i) {
            _cardDeck[i] = Resources.Load<Sprite>($"Cards/card_{i}");
        }

        currentCards = new ReactiveCollection<Sprite>();
        answers = new ReactiveCollection<int[]>();
        currentAnswer = new ReactiveCollection<int>();

        // Set Score Model
        score = new IntReactiveProperty(0);
        combo = new IntReactiveProperty(0);
    }

    private void Start() {
        // Timmer Model
        Observable.Interval(TimeSpan.FromMilliseconds(1))
            .Where(_ => currentCards.Count != 0)
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
