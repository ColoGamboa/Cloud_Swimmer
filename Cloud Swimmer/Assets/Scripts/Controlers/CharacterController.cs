using CloudSwimmer.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controlers
{
    internal class CharacterController2D : MonoBehaviour
    {

        private CharacterMovement _characterMovement;
        private float _horizontal;
        private float _vertical;

        void Start()
        {
            _characterMovement = CharacterMovement.Instance;
        }

        void Update()
        {      
            if (Input.GetKeyDown(KeyCode.W))
            {
                //_characterMovement.Jump();
                _characterMovement.KeyInput('W');
            }
        }
        private void FixedUpdate()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");    
            _vertical = Input.GetAxisRaw("Vertical");
            _characterMovement.Movement(_horizontal, _vertical);
        }
    }
}
