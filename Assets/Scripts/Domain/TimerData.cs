using System;

namespace Domain
{
    [Serializable]
    public record TimerData
    {
        public double remainTime;

        public static TimerData Default => new TimerData() { remainTime = 0.0d, };
    }
}