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
    internal class CloudState : MonoBehaviour, ICharacterState
    {
        public GameObject _char;
        [SerializeField] private float _speed;
        private CharacterMovement _context;
        [SerializeField] int _blocksInside;
        [SerializeField] private bool _onGround;

        public void Init(Collider2D collider, Vector2 _pos, Vector2 _direction)
        {           
            Vector2 localCenter = collider.offset;
            Vector2 worldCenter = (Vector2)collider.transform.position + localCenter;            
            _char.transform.position = worldCenter;
            MakeSelfVisible(_char,true);           
        }
        public void Start()
        {
            _char = gameObject;
            MakeSelfVisible(_char, false);
            _speed = 1.0f;
        }
        public void CheckTriggerEnter(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("CloudBlock"))
            {
                Debug.Log("ENTRANDO EN CLOUD");
            }
            if (collider.gameObject.CompareTag("Ground"))
            {
                _onGround = false;
            }
        }
        public void CheckTriggerExit(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("CloudBlock"))
            {
                StartCoroutine(AwaitExitCollider());
                if (_blocksInside == 0)
                {
                    DeactivateCharacter(_char);
                    var _OutsideState = GetOutsideState();
                    Vector2 _pos = _char.transform.position;
                    Vector2 _direction =_pos.normalized;
                    _OutsideState.Init(collider,_pos, _direction);
                    _char.transform.position = Vector2.zero;
                    _OutsideState.SetContext(_context);
                    SetStateToContext(_OutsideState);   
                }
            }
            if (collider.gameObject.CompareTag("Ground"))
            {
                _onGround = true;
            }
        }
        private IEnumerator AwaitExitCollider()
        {
            yield return new WaitForSeconds(0.05f);
            
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            _blocksInside++;
            CheckTriggerEnter(collider);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            _blocksInside--;
            CheckTriggerExit(other);
        }
        private void DeactivateCharacter(GameObject _char)
        {
            Debug.Log("DESACTIVANDO CHAR");
            MakeSelfVisible(_char, false);
        }
        public void KeyInput(char _key)
        {
            throw new NotImplementedException();
        }
        public void Move(float _horizontal, float _vertical)
        {
            Vector2 direction = new Vector2(_horizontal, _vertical).normalized;
            if (!_onGround)
            {                
                _char.transform.Translate(direction * _speed * Time.deltaTime);
            }
            else
            {
                _char.transform.Translate((direction * -1) * _speed * Time.deltaTime);
                _onGround = false;
            }         
        }
        public void SetContext(CharacterMovement _movement)
        {
            _context = _movement;
        }
        private void MakeSelfVisible(GameObject _char, bool isVisible)
        {
            SpriteRenderer spriteRenderer = _char.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = isVisible;
            }
        }
        private ICharacterState GetOutsideState()
        {
            GameObject _OutsideCharacter = GameObject.FindWithTag("OutsideCharacter");
            if (_OutsideCharacter != null) { Debug.Log("SE ENCONTRO FUERA DE NUBE"); }
            ICharacterState _OutsideState = _OutsideCharacter.GetComponent<OutSideCloudState>();
            return _OutsideState; //revisar que no sean null?
        }
        private void SetStateToContext(ICharacterState _state)
        {
            _context.SetState(_state);
        }
    }
}
