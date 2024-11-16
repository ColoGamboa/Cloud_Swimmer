using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Entities;

namespace CloudSwimmer.Entities
{
    public class CharacterMovement : MonoBehaviour 
    {
        //singleton
        private static CharacterMovement _instance;
        public static CharacterMovement Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject singletonCharacterMovement = new GameObject();
                    _instance = singletonCharacterMovement.AddComponent<CharacterMovement>();
                    singletonCharacterMovement.name = typeof(CharacterMovement).Name + " (Singleton)";
                    DontDestroyOnLoad(singletonCharacterMovement);
                }
                return _instance;
            }
        }
        private void Awake()
        {
            // Si hay otra instancia, destrúyela
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }
        public ICharacterState _charState;
        public void Start()
        {
            _charState = GetComponentInChildren<OutSideCloudState>();
            _charState.SetContext(this);
        }
        public void Movement(float _horizontal, float _vertical) //general
        {
            //Debug.Log("MOVEMENT MOVE");
            _charState.Move(_horizontal, _vertical);            
        }
        public void KeyInput(char _key)
        {
            _charState.KeyInput(_key);
        }
        public void SetState(ICharacterState _state)
        {
            _charState = _state;
        } 
    }
}
