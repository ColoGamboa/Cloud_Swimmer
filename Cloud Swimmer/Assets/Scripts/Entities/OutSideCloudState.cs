using Assets.Scripts.Interfaces;
using CloudSwimmer.Entities;
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
        public GameObject _char;
        private Rigidbody2D _rb;
        public bool _isGrounded;
        public float _moveSpeed = 5f;
        public float _jumpForce;
        public CharacterMovement _context;
        public bool hasCollided = false;


        public void Start() 
        {
            //Instantiate(_char);
            _char = gameObject;
            _rb = GetComponent<Rigidbody2D>();
            _jumpForce = 250f;          
        }
        public void Init(Collider2D collider, Vector2 _position, Vector2 _direction)
        {
            Debug.Log("INICIANDO CHAR");
            Debug.Log("Outside");
            Debug.Log(_direction);
            Debug.Log(_position);
            _position.y += 10f * Time.deltaTime * _direction.y;
            _position.x += 10f * Time.deltaTime * _direction.x;
            _char.transform.position = _position;
            _rb.velocity = _direction;
            MakeSelfVisible(_char, true);
            Debug.Log("DEERIA ESTAR");
            StartCoroutine(SetCollitionEvent());
        }
        private IEnumerator SetCollitionEvent()
        {
            yield return new WaitForSeconds(0.5f); // Retardo pequeño para evitar el "bug"
            hasCollided = false;
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
            if (collider.gameObject.CompareTag("CloudBlock") && !hasCollided)
            {
                deactivateCharacter(_char);
                var _cloudState = GetCloudState();
                _cloudState.Init(collider, Vector2.zero, Vector2.zero);
                _char.transform.position = Vector2.zero;
                _cloudState.SetContext(_context);
                SetStateToContext(_cloudState);
               hasCollided = true;
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
        private void deactivateCharacter(GameObject _char)
        {
            Debug.Log("DESACTIVANDO CHAR");
            MakeSelfVisible(_char, false);           
            //_char.transform.position = Vector2.zero; 
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
            //CloudCharacter
            if (_cloudCharacter != null) { Debug.Log("SE ENCONTRO"); }            
            ICharacterState _cloudState = _cloudCharacter.GetComponent<CloudState>();
            return _cloudState; //revisar que no sean null?
        }
        private void SetStateToContext(ICharacterState _state)
        {
            _context.SetState(_state);
        }
    }
}
