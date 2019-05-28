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

    private int[] PickCards() {
        int[] result = Enumerable.Range(0, 27).ToArray();
        this.Shuffle(result);
        return result;
    }

    private void Shuffle(int[] a) {
        for(int i = a.Length - 1; i > 0; i--) {
            int rnd = UnityEngine.Random.Range(0, i);
            int temp = a[i];

            a[i] = a[rnd];
            a[rnd] = temp;
        }
    }

    private List<int[]> GetAnswer(int[] _pickedCards) {
        List<int[]> result = new List<int[]>();
        int[] temp = new int[3];

        for(int i = 0; i < 9; ++i) {
            for(int j = i+1; j < 9; ++j) {
                for(int k = j+1; k < 9; ++k) {
                    temp[0] = (_pickedCards[i] / 9 + _pickedCards[j] / 9 + _pickedCards[k] / 9) % 3;
                    temp[1] = (_pickedCards[i] % 9 / 3 + _pickedCards[j] % 9 / 3 + _pickedCards[k] % 9 / 3) % 3;
                    temp[2] = (_pickedCards[i] % 3 + _pickedCards[j] % 3 + _pickedCards[k] % 3) % 3;

                    if(temp[0] == 0 && temp[1] == 0 && temp[2] == 0) {
                        result.Add(new int[3] { i, j, k });
                        Debug.Log(i.ToString() + " " + j.ToString() + " " + k.ToString());
                    }
                }
            }
        }

        return result;
    }

    private void GameStart() {
        localGameModel.currentCards.Clear();
        localGameModel.answers.Clear();
        int[] pickedCards = this.PickCards();
        List<int[]> answer = this.GetAnswer(pickedCards);
        for(int i = 0; i < 9; ++i) {
            localGameModel.currentCards.Add(localGameModel.gameBoardModelAsset.cardSpriteList[pickedCards[i]]);
        }
        for(int i = 0; i < answer.Count; ++i) {
            localGameModel.answers.Add(answer[i]);
        }
        gameBoardView.UpdateView(localGameModel.currentCards);
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
