using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class InGamePresenter : MonoBehaviour {

    public GameBoardModel gameBoardModel;
    public GameBoardView gameBoardView;

    private void Start() {

        // Button click event
        gameBoardView.OnClickCards.Subscribe(ButtonClick);

        gameBoardModel.currentAnswer
            .ObserveCountChanged()
            .Where(e => e == 3)
            .Where(e => IsCorrectAnswer())
            .Subscribe(e => {
                gameBoardModel.score.Value += 2;
                gameBoardModel.combo.Value++;

                gameBoardModel.currentAnswer.Clear();
            });
        
        gameBoardModel.currentAnswer
            .ObserveCountChanged()
            .Where(e => e == 3)
            .Where(e => !IsCorrectAnswer())
            .Subscribe(e => {
                gameBoardModel.score.Value -= 2;
                gameBoardModel.combo.Value = 0;

                gameBoardModel.currentAnswer.Clear();
            });
    }

    private bool IsCorrectAnswer() {
        var ret = false;

        ret = gameBoardModel.answers.Any(e => gameBoardModel.currentAnswer.All(x => e.Contains(x)));

        return ret;
    }

    private void ButtonClick(int n) {

        if(gameBoardModel.currentAnswer.Contains(n)) {
            gameBoardModel.currentAnswer.Remove(n);
        }
        else {
            gameBoardModel.currentAnswer.Add(n);
        }
    }
}
