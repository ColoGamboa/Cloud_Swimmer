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
        private bool _insideCloud;
        private float _horizontal;

        void Start()
        {
            _characterMovement = GetComponent<CharacterMovement>(); 
            _insideCloud = false;
        }

        void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!_insideCloud) { _characterMovement.Jump(); }          
            }
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!_insideCloud)
            {
                _characterMovement.CheckTriggerEnter(collider);
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            
            if (!_insideCloud)
            {
                _characterMovement.CheckTriggerExit(collider);
            }
        }      
        private void FixedUpdate()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            if (!_insideCloud) { _characterMovement.HorizontalMove(_horizontal); }
        }
    }
}
