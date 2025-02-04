using System;
using Domain;

namespace Model.Interface
{
    public enum ToggleResult
    {
        ToggleOn,
        ToggleOff,
        AlreadyCompleted,
        SystemError,
    }

    public enum SummitResult
    {
        Correct,
        Incorrect,
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
        public bool IsPickCompleted { get; }
        public int[] DeckList { get; }
        public int DeckSize { get; }
        
        public bool IsPicked(int value);
        public string GetPicked();
        public string GetAnswers();


        // API
        public void StartGame();

        public ToggleResult ToggleCard(int value);
        public SummitResult SubmitPicked();
        public SummitResult SubmitGyeol();

        public void DecreaseTime(TimeSpan value);
        public void ResetTime();
    }
}