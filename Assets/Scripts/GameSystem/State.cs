using System;

namespace GameSystem
{
    public enum GameState
    {
        Main,
        Playing,
        Result,
    }

    public enum PlayingState
    {
        None,
        Playing,
        GameOver,
    }
}