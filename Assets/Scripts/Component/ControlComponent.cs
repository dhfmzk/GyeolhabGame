using GameSystem;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Component
{
    public class ControlComponent : MonoBehaviour
    {
        public Button gyeolButton;

        public void ComponentAwake()
        {
            this.gyeolButton.OnClickAsObservable()
                .Select(_ => GameLoop.I.GameModel.HasRemainAnswer)
                .Subscribe(hasRemainAnswer =>
                {
                    if (hasRemainAnswer)
                    {
                        GameLoop.I.GameModel.DecreaseScore(4);
                    }
                    else
                    {
                        GameLoop.I.GameModel.IncreaseScore(4);
                    }
                })
                .AddTo(this);
        }
    }
}
