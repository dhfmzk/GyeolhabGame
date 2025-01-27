using GameSystem;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Component
{
    public class MainMenuComponent : MonoBehaviour
    {
        private static readonly int IsActive = Animator.StringToHash("IsActive");

        [Header("View")]
        public Button startButton;
        public Animator animator;
        
        private void Start()
        {
            this.startButton.OnClickAsObservable()
                .Subscribe(e =>
                {
                    GameLoop.I.GameModel.StartGame();
                })
                .AddTo(this);
        }

        private void Update()
        {
            var isActive = this.animator.GetBool(IsActive);
            if (!GameLoop.I.GameModel.IsGamePlaying != this.animator.GetBool(IsActive))
            {
                this.animator.SetBool(IsActive, !GameLoop.I.GameModel.IsGamePlaying);
            }
        }
    }
}
