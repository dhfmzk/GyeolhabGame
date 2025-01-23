using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Component
{
    public class BoardComponent : MonoBehaviour
    {
        public GameBoardView gameBoardView;
        public Button gyeolButton;

        public Transform gameMenuCover;

        private void Start() {

            // this.localGameModel.isGamePlaying
            //     .DistinctUntilChanged()
            //     .Where(e => e)
            //     .Subscribe(e => {
            //         // Game Start
            //         // 변수 구독
            //         // 타이머 시작
            //         this.GameStart();
            //     });
            //
            // // Button click event
            // this.gameBoardView.OnClickCards.Subscribe(ButtonClick);
            //
            // this.gyeolButton.OnClickAsObservable()
            //     .Where(_ => this.HasLeftAnswer())
            //     .Subscribe(e => {
            //         this.localGameModel.score.Value -= 4;
            //         this.localGameModel.combo.Value = 0;
            //     });
            //
            // this.gyeolButton.OnClickAsObservable()
            //     .Where(_ => !this.HasLeftAnswer())
            //     .Subscribe(e => {
            //         this.localGameModel.score.Value += 4;
            //         this.localGameModel.combo.Value++;
            //     });
            //
            // // Answer Calc logic
            // var currentAnswerStream = this.localGameModel.currentAnswer
            //     .ObserveCountChanged()
            //     .Where(e => e == 3);
            //
            // currentAnswerStream
            //     .Where(e => this.IsCorrectAnswer())
            //     .Subscribe(e => {
            //         this.localGameModel.score.Value += 2;
            //         this.localGameModel.combo.Value++;
            //         this.localGameModel.currentAnswer.Clear();
            //     });
            //
            // currentAnswerStream
            //     .Where(e => !this.IsCorrectAnswer())
            //     .Subscribe(e => {
            //         this.localGameModel.score.Value -= 2;
            //         this.localGameModel.combo.Value = 0;
            //         this.localGameModel.currentAnswer.Clear();
            //     });
        }

        private int[] PickCards()
        {
            var result = Enumerable.Range(0, 27).ToArray();
            this.Shuffle(result);
            return result;
        }

        private void Shuffle(int[] a)
        {
            for(var i = a.Length - 1; i > 0; i--)
            {
                var rnd = UnityEngine.Random.Range(0, i);
                (a[i], a[rnd]) = (a[rnd], a[i]);
            }
        }

        private static List<int[]> GetAnswer(int[] pickedCards)
        {
            var result = new List<int[]>();
            var temp = new int[3];

            for(var i = 0; i < 9; ++i)
            {
                for(var j = i+1; j < 9; ++j)
                {
                    for(var k = j+1; k < 9; ++k)
                    {
                        temp[0] = (pickedCards[i] / 9 + pickedCards[j] / 9 + pickedCards[k] / 9) % 3;
                        temp[1] = (pickedCards[i] % 9 / 3 + pickedCards[j] % 9 / 3 + pickedCards[k] % 9 / 3) % 3;
                        temp[2] = (pickedCards[i] % 3 + pickedCards[j] % 3 + pickedCards[k] % 3) % 3;

                        if (temp[0] != 0 || temp[1] != 0 || temp[2] != 0)
                        {
                            continue;
                        }
                        
                        result.Add(new int[3] { i, j, k });
                        Debug.Log(i.ToString() + " " + j.ToString() + " " + k.ToString());
                    }
                }
            }

            return result;
        }

        private void GameStart()
        {
            // this.localGameModel.currentCards.Clear();
            // this.localGameModel.answers.Clear();
            // var pickedCards = this.PickCards();
            // var answer = GetAnswer(pickedCards);
            //
            // for(var i = 0; i < 9; ++i)
            // {
            //     this.localGameModel.currentCards.Add(this.localGameModel.gameBoardModelAsset.cardSpriteList[pickedCards[i]]);
            // }
            //
            // for(var i = 0; i < answer.Count; ++i) 
            // {
            //     this.localGameModel.answers.Add(answer[i]);
            // }
            // this.gameBoardView.UpdateView(this.localGameModel.currentCards);
        }

        private bool IsCorrectAnswer()
        {
            var ret = false;

            // ret = this.localGameModel.answers.Any(e => this.localGameModel.currentAnswer.All(x => e.Contains(x)));

            return ret;
        }
    
        private bool HasLeftAnswer()
        {
            var ret = false;

            // ret = this.localGameModel.answers.Count > 0;

            return ret;
        }

        private void ButtonClick(int n)
        {
            // if(this.localGameModel.currentAnswer.Contains(n))
            // {
            //     this.localGameModel.currentAnswer.Remove(n);
            // }
            // else
            // {
            //     this.localGameModel.currentAnswer.Add(n);
            // }
        }
    }
}
