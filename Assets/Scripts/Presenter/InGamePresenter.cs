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

        gameBoardView.OnClickCards.Subscribe(ButtonClick);
    }

    private void ButtonClick(int n) {
        if(gameBoardModel.currentAnswer.Contains(n)) {
            gameBoardModel.currentAnswer.Remove(n);
        }
        else {
            gameBoardModel.currentAnswer.Add(n);
        }


        if(gameBoardModel.currentAnswer.Count == 3) {
            int tempScore = gameBoardModel.score.Value - 2;
            int tempCombo = 0;
            for(int i = 0; i < gameBoardModel.answers.Count; ++i) {
                if(gameBoardModel.currentAnswer.Contains(gameBoardModel.answers[i][0])
                    && gameBoardModel.currentAnswer.Contains(gameBoardModel.answers[i][1])
                    && gameBoardModel.currentAnswer.Contains(gameBoardModel.answers[i][2])) {
                    Debug.Log("정답");
                    tempCombo = gameBoardModel.combo.Value + 1;
                    tempScore += (tempCombo + 2);
                    gameBoardModel.answers.RemoveAt(i);
                    break;
                }
            }
            gameBoardModel.combo.Value = tempCombo;
            gameBoardModel.score.Value = tempScore;

            gameBoardModel.currentAnswer.Clear();

        }
    }
}
