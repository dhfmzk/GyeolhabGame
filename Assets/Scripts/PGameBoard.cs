using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PGameBoard : MonoBehaviour {

    private MGameBoard model;

    [SerializeField]
    private Button[] cards = new Button[9];

    private void Awake() {
    }

    private void Start() {
        model = MGameBoard.getInstance();

        model.currentCards
            .ObserveAdd()
            .Subscribe(x => {
                cards[x.Index].image.overrideSprite = x.Value;
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

    private void ButtonClick(int i) {
        if(model.currentAnswer.Contains(i)) {
            model.currentAnswer.Remove(i);
        }
        else {
            model.currentAnswer.Add(i);
        }
    }
}
