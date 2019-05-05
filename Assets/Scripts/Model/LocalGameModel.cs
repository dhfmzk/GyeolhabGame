using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LocalGameModel : MonoBehaviour {

    // Models for Game
    public BoolReactiveProperty isGamePlaying;

    // Model for Timer
    public const int MAX_TIME = 3000;
    public int maxTime;
    public IntReactiveProperty timerReactiveProperty = new IntReactiveProperty(MAX_TIME);

    // Model for GameBoard
    public GameBoardModelAsset gameBoardModelAsset;
    public ReactiveCollection<Sprite> currentCards = new ReactiveCollection<Sprite>();
    public ReactiveCollection<int[]> answers = new ReactiveCollection<int[]>();
    public ReactiveCollection<int> currentAnswer = new ReactiveCollection<int>();

    // Model for Score
    public IntReactiveProperty score = new IntReactiveProperty();
    public IntReactiveProperty combo = new IntReactiveProperty();
    public IntReactiveProperty maxCombo = new IntReactiveProperty();


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
                    timerReactiveProperty.Value = MAX_TIME;
                }
            })
            .AddTo(this);
    }

    public void ResetGame() {
        timerReactiveProperty.Value = MAX_TIME;
    }
}
