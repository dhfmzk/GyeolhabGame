using Domain;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class TimerView : MonoBehaviour
    {
        public Image timerGauge;

        public void UpdateView(in TimerData data)
        {
            this.timerGauge.fillAmount = (float)data.remainTime;
        }
    }
}
