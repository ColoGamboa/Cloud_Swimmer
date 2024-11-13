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
        public float _speed;
        public CharacterMovement _context;
        public int _blocksInside;
        float _horizontal;
        float _vertical;
        //private float _lastHorizontalInput = 0f;
        //private float _lastVerticalInput = 0f;

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
            // Verifica si el objeto que entra tiene el tag "CloudBlock"
            if (collider.gameObject.CompareTag("CloudBlock"))
            {
                Debug.Log("ENTRANDO EN CLOUD");
                //_blocksInside++; // Incrementa el contador
            }
        }

        public void CheckTriggerExit(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("CloudBlock"))
            {
                StartCoroutine(AwaitExitCollider());
                if (_blocksInside == 0)
                {
                    deactivateCharacter(_char);
                    var _OutsideState = GetOutsideState();
                    Vector2 _pos = _char.transform.position;
                    Vector2 _direction =_pos.normalized;
                    Debug.Log("Cloud");
                    Debug.Log(_direction);
                    Debug.Log(_pos);
                    _OutsideState.Init(collider,_pos, _direction);
                    _char.transform.position = Vector2.zero;
                    _OutsideState.SetContext(_context);
                    SetStateToContext(_OutsideState);   
                }
            }
        }
        private IEnumerator AwaitExitCollider()
        {
            yield return new WaitForSeconds(0.05f); // Retardo pequeño para evitar el "bug"
            
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            _blocksInside++;
            CheckTriggerEnter(collider);
        }

        /*private void OnTriggerStay2D(Collider2D other)
        {
            // Esto podría ser útil si necesitas realizar acciones mientras está en colisión
            if (other.CompareTag("CloudBlock"))
            {
                //Debug.Log("Permaneciendo dentro de un CloudBlock.");
            }
        }*/

        private void OnTriggerExit2D(Collider2D other)
        {
            _blocksInside--; // Decrementa el contador
            // Verifica si el objeto que sale tiene el tag "CloudBlock"
            CheckTriggerExit(other);
        }
        private void deactivateCharacter(GameObject _char)
        {
            Debug.Log("DESACTIVANDO CHAR");
            MakeSelfVisible(_char, false);
            //_char.transform.position = Vector2.zero;
        }

        public void KeyInput(char _key)
        {
            throw new NotImplementedException();
        }

        public void Move(float _horizontal, float _vertical)
        {
            //Debug.Log("CLOUD MOVE");
            Vector2 direction = new Vector2(_horizontal, _vertical).normalized;
            //Debug.Log("Dirección: " + direction + ", Velocidad: " + _speed + ", DeltaTime: " + Time.deltaTime);
            _char.transform.Translate(direction * _speed * Time.deltaTime);
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
                spriteRenderer.enabled = isVisible; // Hacer el sprite invisible
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
        public void FixedUpdate()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
            //if (_horizontal != 0f)
            //{
            //    _lastHorizontalInput = _horizontal;
            //}

            //if (_vertical != 0f)
            //{
            //    _lastVerticalInput = _vertical;
            //}
        }
    }
}
