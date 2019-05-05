using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameMenuPresenter : MonoBehaviour {

    public LocalGameModel localGameModel;

    public Button startButton;

    private Animator animator => GetComponent<Animator>();
    private void Start() {

        startButton.OnClickAsObservable()
            .Subscribe(e => {
                Debug.Log($"On Click Start Button");
                localGameModel.isGamePlaying.Value = true;
            });

        localGameModel.isGamePlaying
            .Subscribe(e => {
                animator.SetBool("test", e);
            });
    }
}
