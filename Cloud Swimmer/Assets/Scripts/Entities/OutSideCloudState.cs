using Assets.Scripts.Interfaces;
using CloudSwimmer.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    internal class OutSideCloudState : MonoBehaviour, ICharacterState
    {
        //public GameObject _char;
        private Rigidbody2D _rb;
        public bool _isGrounded;
        public float _moveSpeed = 5f;
        public float _jumpForce;
        private CharacterMovement _context;

        public void Start() 
        {
            //Instantiate(_char);
            _rb = GetComponent<Rigidbody2D>();
            _jumpForce = 250f;          
        }
        public void SetContext(CharacterMovement _movement)
        {
            _context = _movement;
        }
        public void Move(float _horizontal, float _vertical)
        {
            _rb.velocity = new Vector2(_horizontal, _rb.velocity.y);
        }
        public void CheckTriggerEnter(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Ground"))
            {
                StartCoroutine(SetGroundedWithDelay());
            }
        }
        private IEnumerator SetGroundedWithDelay()
        {
            yield return new WaitForSeconds(0.05f); // Retardo pequeño para evitar el "bug"
            _isGrounded = true;
        }
        public void CheckTriggerExit(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Ground"))
            {
                _isGrounded = false;
            }
        }
        public void KeyInput(char _key) 
        {
            Debug.Log("KEYINPUT");
            if (_isGrounded && (_key == 'W')) { Jump(); }
        }
        public void Jump()
        {
            Debug.Log("JUMP");
            _rb.AddForce(Vector2.up * _jumpForce);
            _isGrounded = false;
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            Debug.Log("Trigger");
            CheckTriggerEnter(collider);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            CheckTriggerExit(collider);
        }
    }
}
