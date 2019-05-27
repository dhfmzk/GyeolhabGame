using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class InGamePresenter : MonoBehaviour {

    public LocalGameModel localGameModel;
    public GameBoardView gameBoardView;
    public Button gyeolButton;

    public Transform gameMenuCover;

    private void Start() {

        localGameModel.isGamePlaying
            .DistinctUntilChanged()
            .Where(e => e)
            .Subscribe(e => {
                // Game Start
                // 변수 구독
                // 타이머 시작
                GameStart();
            });
        
        // Button click event
        gameBoardView.OnClickCards.Subscribe(ButtonClick);
        
        gyeolButton.OnClickAsObservable()
            .Where(_ => HasLeftAnswer())
            .Subscribe(e => {
                localGameModel.score.Value -= 4;
                localGameModel.combo.Value = 0;
            });

        gyeolButton.OnClickAsObservable()
            .Where(_ => !HasLeftAnswer())
            .Subscribe(e => {
                localGameModel.score.Value += 4;
                localGameModel.combo.Value++;
            });

        // Answer Calc logic
        var currentAnswerStream = localGameModel.currentAnswer
                                    .ObserveCountChanged()
                                    .Where(e => e == 3);
        
        currentAnswerStream
            .Where(e => IsCorrectAnswer())
            .Subscribe(e => {
                localGameModel.score.Value += 2;
                localGameModel.combo.Value++;
                localGameModel.currentAnswer.Clear();
            });
        
        currentAnswerStream
            .Where(e => !IsCorrectAnswer())
            .Subscribe(e => {
                localGameModel.score.Value -= 2;
                localGameModel.combo.Value = 0;
                localGameModel.currentAnswer.Clear();
            });
    }

    private void GameStart() {
    }

    private bool IsCorrectAnswer() {
        var ret = false;

        ret = localGameModel.answers.Any(e => localGameModel.currentAnswer.All(x => e.Contains(x)));

        return ret;
    }
    
    private bool HasLeftAnswer() {
        var ret = false;

        ret = localGameModel.answers.Count > 0;

        return ret;
    }

    private void ButtonClick(int n) {

        if(localGameModel.currentAnswer.Contains(n)) {
            localGameModel.currentAnswer.Remove(n);
        }
        else {
            localGameModel.currentAnswer.Add(n);
        }
    }
}
