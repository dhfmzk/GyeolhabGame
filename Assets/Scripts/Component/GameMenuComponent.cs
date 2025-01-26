using GameSystem;
using Model;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Component
{
    public class GameMenuComponent : MonoBehaviour
    {
        public GameModel gameModel;

        public Button startButton;

        private Animator Animator => this.GetComponent<Animator>();
        private void Start()
        {
            this.startButton.OnClickAsObservable()
                .Subscribe(e =>
                {
                    GameLoop.I.GameModel.StartGame();
                });

            // this.gameModel.isGamePlaying
            //     .DistinctUntilChanged()
            //     .Subscribe(e => {
            //         this.Animator.SetBool("isActive", !e);
            //     });
        }
    }
}
