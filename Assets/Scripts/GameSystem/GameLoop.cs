using System;
using System.Collections.Generic;
using System.Linq;
using Component.Attribute;
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
        private List<GameObject> awakableGameObjects;
        private List<IAwakableComponent> awakableComponents = new();
        
        [SerializeField]
        private List<GameObject> updatableGameObjects;
        private List<IUpdatableComponent> updatableComponents = new();
        
        public GameSetting GameSetting => this.gameSetting;
        public IGameModel GameModel => this.gameModel;

        public void Awake()
        {
            this.SingletonInit();
            
            foreach (var gameObject in this.awakableGameObjects.Where(e => e != null))
            {
                var awakableComponents = gameObject.GetComponents<IAwakableComponent>();
                
                foreach (var awakableComponent in awakableComponents)
                {
                    this.awakableComponents.Add(awakableComponent);
                }
            }
            
            foreach (var gameObject in this.updatableGameObjects.Where(e => e != null))
            {
                var updatableComponents = gameObject.GetComponents<IUpdatableComponent>();
                
                foreach (var updatableComponent in updatableComponents)
                {
                    this.updatableComponents.Add(updatableComponent);
                }
            }
        }

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
