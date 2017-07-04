using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PStarter : MonoBehaviour {

    private MGameBoard model;

    [SerializeField]
    private Button startButton;

    private void Awake() {
    }

    private void Start() {
        model = MGameBoard.getInstance();

        startButton.OnClickAsObservable()
            .Subscribe(_ => {
                model.currentCards.Clear();
                model.answers.Clear();
                int[] pickedCards = this.PickCards();
                List<int[]> answer = this.GetAnswer(pickedCards);
                for(int i = 0; i < 9; ++i) {
                    model.currentCards.Add(model._cardDeck[pickedCards[i]]);
                }
                for(int i = 0; i < answer.Count; ++i) {
                    model.answers.Add(answer[i]);
                }
            });
    }

    private int[] PickCards() {
        int[] result = Enumerable.Range(0, 27).ToArray();
        this.Shuffle(result);
        return result;
    }

    private void Shuffle(int[] a) {
        for(int i = a.Length - 1; i > 0; i--) {
            int rnd = Random.Range(0, i);
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
}

