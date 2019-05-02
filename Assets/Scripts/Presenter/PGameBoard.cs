using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PGameBoard : MonoBehaviour {

    private MGameBoard model;

    [SerializeField]
    private Button[] cards = new Button[9];

    [SerializeField]
    private List<Image> currentAnswerImage;

    private List<int> currentAnswer = new List<int>();

    private void Awake() {
    }

    private void Start() {
        model = MGameBoard.getInstance();

        model.currentCards
            .ObserveAdd()
            .Subscribe(x => {
                cards[x.Index].image.sprite = x.Value;
            });
        
        // TODO : Make 1 view layer & merge this streams to 1 stream in view layer
        cards[0].OnClickAsObservable().Subscribe(_ => ButtonClick(0));
        cards[1].OnClickAsObservable().Subscribe(_ => ButtonClick(1));
        cards[2].OnClickAsObservable().Subscribe(_ => ButtonClick(2));
        cards[3].OnClickAsObservable().Subscribe(_ => ButtonClick(3));
        cards[4].OnClickAsObservable().Subscribe(_ => ButtonClick(4));
        cards[5].OnClickAsObservable().Subscribe(_ => ButtonClick(5));
        cards[6].OnClickAsObservable().Subscribe(_ => ButtonClick(6));
        cards[7].OnClickAsObservable().Subscribe(_ => ButtonClick(7));
        cards[8].OnClickAsObservable().Subscribe(_ => ButtonClick(8));
    }

    private void ButtonClick(int n) {
        if(this.currentAnswer.Contains(n)) {
            this.currentAnswer.Remove(n);
        }
        else {
            this.currentAnswer.Add(n);
        }

        for(int i = 0; i < this.currentAnswer.Count; ++i) {
            currentAnswerImage[i].overrideSprite = cards[currentAnswer[i]].image.sprite;
        }

        if(this.currentAnswer.Count == 3) {
            int tempScore = model.score.Value - 2;
            int tempCombo = 0;
            for(int i = 0; i < model.answers.Count; ++i) {
                if(this.currentAnswer.Contains(model.answers[i][0])
                    && this.currentAnswer.Contains(model.answers[i][1])
                    && this.currentAnswer.Contains(model.answers[i][2])) {
                    Debug.Log("정답");
                    tempCombo = model.combo.Value + 1;
                    tempScore += (tempCombo + 2);
                    model.answers.RemoveAt(i);
                    break;
                }
            }
            model.combo.Value = tempCombo;
            model.score.Value = tempScore;

            for(int i = 0; i < this.currentAnswer.Count; ++i) {
                currentAnswerImage[i].overrideSprite = null;
            }

            this.currentAnswer.Clear();

        }
    }

}
