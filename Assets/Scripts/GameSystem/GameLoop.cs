using System;
using System.Collections.Generic;
using System.Linq;
using Component.Interface;
using Model;
using Model.Interface;
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
        [SerializeField]
        private List<GameObject> startableGameObjects;
        private List<IStartableComponent> _startableComponents = new();
        
        [SerializeField]
        private List<GameObject> updatableGameObjects;
        private List<IUpdatableComponent> _updatableComponents = new();
        
        public GameSetting GameSetting => this.gameSetting;
        public IGameModel GameModel => this.gameModel;

        public void Awake()
        {
            this.SingletonInit();
            
            foreach (var gameObject in this.startableGameObjects.Where(e => e != null))
            {
                var startableComponents = gameObject.GetComponents<IStartableComponent>();
                
                foreach (var awakableComponent in startableComponents)
                {
                    this._startableComponents.Add(awakableComponent);
                }
            }
            
            foreach (var gameObject in this.updatableGameObjects.Where(e => e != null))
            {
                var updatableComponents = gameObject.GetComponents<IUpdatableComponent>();
                
                foreach (var updatableComponent in updatableComponents)
                {
                    this._updatableComponents.Add(updatableComponent);
                }
            }
        }

        public void Start()
        {
            foreach (var component in this._startableComponents)
            {
                component.ComponentStart();
            }
        }

        public void Update()
        {
            var deltaTime = TimeSpan.FromSeconds(Time.deltaTime);
            
            foreach (var component in this._updatableComponents)
            {
                component.ComponentUpdate(deltaTime);
            }
        }
        
        public static GameLoop I { get; private set; }
        private void SingletonInit()
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
