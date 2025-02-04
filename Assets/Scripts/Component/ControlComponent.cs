using GameSystem;
using Model.Interface;
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
                    var result = GameLoop.I.GameModel.SubmitGyeol();
                    if (result != SummitResult.Correct)
                    {
                        // TODO
                    }
                })
                .AddTo(this);
        }
    }
}
