using System;
using Model;
using R3;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private GameSetting gameSetting;
        [SerializeField] private GameModel gameModel;

        public GameSetting GameSetting => this.gameSetting;
        public IGameModel GameModel => this.gameModel;

        public void Start()
        {
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;
            
            if (this.GameModel.IsGamePlaying)
            {
                this.GameModel.DecreaseTime(deltaTime);

                if (this.GameModel.RemainTurnTime <= 0.0f)
                {
                    this.GameModel.ResetTime();
                }
            }
        }
        
        public static GameLoop I { get; private set; }
        private void Awake()
        {
            if (I == null)
            {
                I = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
