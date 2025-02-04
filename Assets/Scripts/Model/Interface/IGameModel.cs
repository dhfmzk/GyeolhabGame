using System;
using Domain;

namespace Model.Interface
{
    public enum SelectResult
    {
        ToggleOn,
        ToggleOff,
        AlreadyCompleted,
        SystemError,
    }
    
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
        public string GetPicked();


        // API
        public void StartGame();

        public SelectResult TryToggleCard(int value);
        public bool TrySubmitAnswer();
        
        public void DecreaseScore(int value);
        public void IncreaseScore(int value);

        public void DecreaseTime(TimeSpan value);
        public void ResetTime();
    }
}