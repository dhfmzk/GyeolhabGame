using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using GameSystem;
using Model.Interface;

namespace Model
{
    [Serializable]
    public class GameModel : IGameModel
    {
        public float resetTime = 3000.0f;
        private GameState _gameState { get; set; }
        private PlayingState _playingState { get; set; }
        private TimeSpan _remainTurnTime { get; set; }
        
        private int _score { get; set; }
        private int _combo { get; set; }

        // Card Data
        private DeckData Deck { get; set; }
        private List<AnswerData> Answers { get; set; }
        private AnswerData PickedAnswer { get; set; }

        public GameState GameState => this._gameState;
        public PlayingState PlayingState => this._playingState;

        public int Score => this._score;
        public int Combo => this._combo;
        public TimeSpan RemainTurnTime => this._remainTurnTime;
        public bool IsGamePlaying => this.GameState == GameState.Playing;
        
        public GameModel()
        {
            this._gameState = GameState.Main;
            this._playingState = PlayingState.None;

            this.Deck = new DeckData(9);
            this.Answers = new List<AnswerData>();
            this.PickedAnswer = new AnswerData();
        }

#region Query
        
        public bool IsPickCompleted => this.PickedAnswer.IsCompleted;
        
        public bool HasRemainAnswer => this.Answers.Count > 0;

        public int[] DeckList => this.Deck.Cards;
        public int DeckSize => this.Deck?.Cards?.Length ?? 0;
        
        public bool IsPicked(int value) => this.PickedAnswer.Contains(value);
        
        // Temp
        public string GetPicked()
        {
            return this.PickedAnswer.ToString();
        }

        public string GetAnswers()
        {
            return this.Answers.Aggregate(string.Empty, (current, data) => current + $"{data}, ");
        }

#endregion

#region API

        public void StartGame()
        {
            this._gameState = GameState.Playing;

            this.GenerateNewDeck();
        }

        public void DecreaseTime(TimeSpan value)
        {
            this._remainTurnTime -= value;
        }

        public void ResetTime()
        {
            this._remainTurnTime = TimeSpan.FromSeconds(this.resetTime);
        }
        
        /// <summary>
        /// Generates a new deck and calculates possible answers.
        /// </summary>
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

                        var newData = new AnswerData();
                        newData.Add(this.Deck.Cards[i]);
                        newData.Add(this.Deck.Cards[j]);
                        newData.Add(this.Deck.Cards[k]);
                        
                        this.Answers.Add(newData);
                    }
                }
            }
            
            // Step 2) Reset Picked Answer
            this.PickedAnswer.Reset();
        }
        
        /// <summary>
        /// Toggles the selection state of a card at the specified index.
        /// </summary>
        /// <param name="index">The index of the card to toggle.</param>
        /// <returns>
        /// A <see cref="ToggleResult"/> indicating the result of the toggle operation:
        /// <list type="bullet">
        /// <item><description><see cref="ToggleResult.AlreadyCompleted"/> if the picked answer is already completed.</description></item>
        /// <item><description><see cref="ToggleResult.ToggleOn"/> if the card was successfully added to the picked answer.</description></item>
        /// <item><description><see cref="ToggleResult.ToggleOff"/> if the card was successfully removed from the picked answer.</description></item>
        /// <item><description><see cref="ToggleResult.SystemError"/> if there was an error during the toggle operation.</description></item>
        /// </list>
        /// </returns>
        public ToggleResult ToggleCard(int index)
        {
            var value = this.Deck.Cards[index];
            
            if (this.PickedAnswer.IsCompleted)
            {
                return ToggleResult.AlreadyCompleted; // Already Completed
            }
            
            if (!this.PickedAnswer.Contains(value))
            {
                if (this.PickedAnswer.Add(value))
                {
                    return ToggleResult.ToggleOn;
                }
            }
            else
            {
                if (this.PickedAnswer.Remove(value))
                {
                    return ToggleResult.ToggleOff;
                }
            }
            
            return ToggleResult.SystemError;
        }
        
        /// <summary>
        /// Submits the picked answer and returns the result.
        /// </summary>
        /// <returns>
        /// A <see cref="SummitResult"/> indicating whether the submitted answer is correct or incorrect.
        /// </returns>
        public SummitResult SubmitPicked()
        {
            var result = SummitResult.Incorrect;
            
            if (!this.HasRemainAnswer)
            {
                result = SummitResult.Incorrect;
            }

            if (this.Answers.Any(e => e.IsEquals(this.PickedAnswer)))
            {
                result = SummitResult.Correct;
            }

            if (result == SummitResult.Correct)
            {
                this._score += this.GetCalculatedPoint(false);
                this._combo++;
            }
            else
            {
                this._score += -1;
                this._combo = 0;
            }

            this.PickedAnswer.Reset();

            return result;
        }
        
        public SummitResult SubmitGyeol()
        {
            var result = this.HasRemainAnswer ? SummitResult.Correct : SummitResult.Incorrect;
            
            if (result == SummitResult.Correct)
            {
                this._score += this.GetCalculatedPoint(true);
                this._combo++;
            }
            else
            {
                this._score += -2;
                this._combo = 0;
            }

            this.PickedAnswer.Reset();
            
            return result;
        }
        
#endregion

#region Helper

        private int GetCalculatedPoint(bool isGyeol)
        {
            var result = 1;

            if (this._combo > 0)
            {
                result += 1;
            }

            if (this._combo > 10)
            {
                result += 1;
            }

            return result;
        }

#endregion

    }
}
