using System;
using Domain;

namespace Model.Interface
{
    public interface IGameModel
    {
        // State
        public bool IsGamePlaying { get; }
        public TimeSpan RemainTurnTime { get; }

        public int Score { get; }
        public int Combo { get; }


        // Query
        public bool HasRemainAnswer { get; }
        public bool IsCompletePicked { get; }
        public int[] DeckList { get; }
        public int DeckSize { get; }
        
        public bool IsPicked(int value);



        // API
        public void StartGame();

        public bool TryPickCard(int value);
        public bool TrySubmitAnswer();
        
        public void DecreaseScore(int value);
        public void IncreaseScore(int value);

        public void DecreaseTime(TimeSpan value);
        public void ResetTime();
    }
}