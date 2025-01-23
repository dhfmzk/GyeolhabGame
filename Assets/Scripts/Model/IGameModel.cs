namespace Model
{
    public interface IGameModel
    {
        public bool IsGamePlaying { get; }
        public float RemainTurnTime { get; }

        public void StartGame();

        public void DecreaseTime(float value);
        public void ResetTime();
    }
}