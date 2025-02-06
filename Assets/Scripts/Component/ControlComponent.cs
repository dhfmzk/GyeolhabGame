using Component.Interface;
using GameSystem;
using Model.Interface;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Component
{
    public class ControlComponent : MonoBehaviour, IStartableComponent
    {
        public Button gyeolButton;

        public void ComponentStart()
        {
            this.gyeolButton.OnClickAsObservable()
                .Where(_ => GameLoop.I.GameModel.IsGamePlaying)
                .Subscribe(hasRemainAnswer =>
                {
                    var result = GameLoop.I.GameModel.SubmitGyeol();
                    if (result == SummitResult.Correct)
                    {
                        GameLoop.I.GameModel.FinishGame();
                    }
                })
                .AddTo(this);
        }
    }
}
