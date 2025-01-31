using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using GameSystem;
using Model.Interface;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class GameModel : IGameModel
    {
        // Settings
        public float resetTime = 3000.0f;

        // State
        public GameState GameState { get; private set; }
        public PlayingState PlayingState { get; private set; }
        
        // Timer
        public TimeSpan RemainTurnTime { get; private set; }
        
        // Score
        public int Score { get; private set; }
        public int Combo { get; private set; }

        // Card Data
        public DeckData Deck { get; private set; }
        public List<AnswerData> Answers { get; private set; }
        public AnswerData PickedAnswer { get; private set; }

        public GameModel()
        {
            this.GameState = GameState.Main;
            this.PlayingState = PlayingState.None;

            this.Deck = new DeckData(9);
            this.Answers = new List<AnswerData>();
            this.PickedAnswer = new AnswerData();
        }

        public bool IsGamePlaying => this.GameState == GameState.Playing;
        public void StartGame()
        {
            this.GameState = GameState.Playing;
        }

        public void DecreaseTime(TimeSpan value)
        {
            this.RemainTurnTime -= value;
        }

        public void ResetTime()
        {
            this.RemainTurnTime = TimeSpan.FromSeconds(this.resetTime);
        }

        private void GenerateNewDeck()
        {
            // Step 0) Reset Deck
            this.Deck.Reset();
            
            // Step 1) Calculate Answer
            this.Answers.Clear();
            var temp = new int[3];

            for(var i = 0; i < 9; ++i)
            {
                for(var j = i+1; j < 9; ++j)
                {
                    for(var k = j+1; k < 9; ++k)
                    {
                        temp[0] = (this.Deck[i] / 9 + this.Deck[j] / 9 + this.Deck[k] / 9) % 3;
                        temp[1] = (this.Deck[i] % 9 / 3 + this.Deck[j] % 9 / 3 + this.Deck[k] % 9 / 3) % 3;
                        temp[2] = (this.Deck[i] % 3 + this.Deck[j] % 3 + this.Deck[k] % 3) % 3;

                        if (temp[0] != 0 || temp[1] != 0 || temp[2] != 0)
                        {
                            continue;
                        }
                        
                        this.Answers.Add(new AnswerData
                        {
                            Answer1 = temp[0],
                            Answer2 = temp[1],
                            Answer3 = temp[2],
                        });
                    }
                }
            }
            
            // Step 2) Reset Picked Answer
            this.PickedAnswer.Reset();
        }

        public bool TryPickCard(int value)
        {
            // CASE 0) Already Picked
            if (this.PickedAnswer.Contain(value))
            {
                return this.PickedAnswer.Remove(value);
            }
            
            // CASE 1) New Pick
            if (!this.PickedAnswer.IsCompleted)
            {
                return this.PickedAnswer.Add(value);
            }
            
            return false;
        }

        public bool TrySubmitAnswer()
        {
            if (this.HasRemainAnswer)
            {
            }

            if (this.IsCorrectAnswer())
            {
                return true;
            }

            return false;
        }

        public bool IsCompletePicked => this.PickedAnswer.IsCompleted;
        
        public bool HasRemainAnswer => this.Answers.Count > 0;

        private void GameStart()
        {
            // localGameModel.currentCards.Clear();
            // localGameModel.answers.Clear();
            // var pickedCards = this.PickCards();
            // var answer = this.GetAnswer(pickedCards);
            // for(var i = 0; i < 9; ++i)
            // {
            //     localGameModel.currentCards.Add(localGameModel.gameBoardModelAsset.cardSpriteList[pickedCards[i]]);
            // }
            //
            // foreach (var t in answer)
            // {
            //     localGameModel.answers.Add(t);
            // }
            //
            // gameBoardView.UpdateView(localGameModel.currentCards);
        }

        public bool IsCorrectAnswer()
        {
            foreach (var answerData in this.Answers)
            {
                if (answerData.Equals(this.PickedAnswer.Answer1, this.PickedAnswer.Answer2, this.PickedAnswer.Answer3))
                {
                    return true;
                }
            }

            return false;
        }

        public void IncreaseScore(int value)
        {
            this.Score += value;
            this.Combo += 1;
        }
        
        public void DecreaseScore(int value)
        {
            this.Score -= value;
            this.Combo = 0;
        }
    }
}
