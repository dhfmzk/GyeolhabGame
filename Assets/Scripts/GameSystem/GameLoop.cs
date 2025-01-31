using System;
using System.Collections.Generic;
using Component.Interface;
using Model;
using Model.Interface;
using R3;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {
        [Header("Game Setting")]
        [SerializeField] 
        private GameSetting gameSetting;
        
        [Space]
        
        [Header("Game Model")]
        [SerializeField] 
        private GameModel gameModel;
        
        [Space]
        
        [Header("Game Managable Components")]
        [SerializeReference] 
        private List<IAwakableComponent> awakableComponents;
        
        [SerializeReference]
        private List<IUpdatableComponent> updatableComponents;
        
        public GameSetting GameSetting => this.gameSetting;
        public IGameModel GameModel => this.gameModel;

        public void Start()
        {
            foreach (var component in this.awakableComponents)
            {
                component.ComponentAwake();
            }
        }

        public void Update()
        {
            var deltaTime = TimeSpan.FromSeconds(Time.deltaTime);
            
            foreach (var component in this.updatableComponents)
            {
                component.ComponentUpdate(deltaTime);
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
