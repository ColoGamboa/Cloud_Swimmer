using Assets.Scripts.Interfaces;
using CloudSwimmer.Entities;
using CloudSwimmer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    internal class OutSideCloudState : MonoBehaviour, ICharacterState
    {
        private GameObject _char;
        private Rigidbody2D _rb;
        public bool _isGrounded;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpForce;
        private CharacterMovement _context;
        private bool _hasCollided = false;
        [SerializeField] private float _groundWaitingTime;
        [SerializeField] private float _cloudWaitingTime;

        public void Start() 
        {
            _char = gameObject;
            _rb = GetComponent<Rigidbody2D>();
            _jumpForce = 250f;
            _groundWaitingTime = 0.05f;
            _cloudWaitingTime = 0.1f;
        }
        public void Init(Collider2D collider, Vector2 _position, Vector2 _direction)
        {
            _position.y += 10f * Time.deltaTime * _direction.y;
            _position.x += 10f * Time.deltaTime * _direction.x;
            _char.transform.position = _position;
            _rb.linearVelocity = _direction;
            MakeSelfVisible(_char, true);
            StartCoroutine(SetCollitionEvent());
        }
        private IEnumerator SetCollitionEvent()
        {
            yield return new WaitForSeconds(_cloudWaitingTime);
            _hasCollided = false;
        }
        public void SetContext(CharacterMovement _movement)
        {
            _context = _movement;
        }
        public void Move(float _horizontal, float _vertical)
        {
            _rb.linearVelocity = new Vector2(_horizontal * _moveSpeed, _rb.linearVelocity.y);
        }
        public void CheckTriggerEnter(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Ground"))
            {
                StartCoroutine(SetGroundedWithDelay());
            }
            if (collider.gameObject.CompareTag("CloudBlock") && !_hasCollided)
            {
                DeactivateCharacter(_char);
                var _cloudState = GetCloudState();
                _cloudState.Init(collider, Vector2.zero, Vector2.zero);
                _char.transform.position = Vector2.zero;
                _cloudState.SetContext(_context);
                SetStateToContext(_cloudState);
                _hasCollided = true;
            }
        }
        private IEnumerator SetGroundedWithDelay()
        {
            yield return new WaitForSeconds(_groundWaitingTime);
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
            if (_isGrounded && (_key == 'W')) { Jump(); }
        }
        public void Jump()
        {
            _rb.AddForce(Vector2.up * _jumpForce);
            _isGrounded = false;
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            CheckTriggerEnter(collider);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            CheckTriggerExit(collider);
        }
        private void DeactivateCharacter(GameObject _char)
        {
            MakeSelfVisible(_char, false);           
        }
        private void MakeSelfVisible(GameObject _char, bool isVisible)
        {
            SpriteRenderer spriteRenderer = _char.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = isVisible; // Hacer el sprite invisible
            }
        }

        private ICharacterState GetCloudState()
        {
            GameObject _cloudCharacter = GameObject.FindWithTag("CloudCharacter");            
            ICharacterState _cloudState = _cloudCharacter.GetComponent<CloudState>();
            return _cloudState;
        }
        private void SetStateToContext(ICharacterState _state)
        {
            _context.SetState(_state);
        }
    }
}
