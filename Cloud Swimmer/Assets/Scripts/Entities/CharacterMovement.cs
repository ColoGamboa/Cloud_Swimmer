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

                    // No destruir el objeto al cargar nuevas escenas
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
            // Si necesitas inicializar algo aquí, hazlo
        }


        public ICharacterState _charState;

        public void Start()
        {
            _charState = GetComponentInChildren<OutSideCloudState>();
            _charState.SetContext(this);
        }
        public void Movement(float _horizontal, float _vertical) //general
        {
            Debug.Log("MOVEMENT MOVE");
            _charState.Move(_horizontal, _vertical);            
        }
        public void KeyInput(char _key)
        {
            _charState.KeyInput(_key);
        }
        public void SetState(ICharacterState _state)
        {
            _charState = _state;
            Debug.Log($"NUEVO ESTADO $_state");
        } 
    }
}
/*
 * public float _moveSpeed = 5f;       // Velocidad de movimiento
        public float _jumpForce = 300f;      // Fuerza de salto
        public GameObject _cloudChar;
        public float _coreMoveSpeed = 1f;

        private GameObject _char;
        private Rigidbody2D _rb;
        public bool _isGrounded; //hacer privadas 
        public bool _insideCloud;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _insideCloud = false;
            _char = gameObject;
        }
        public void Jump()
        {
            Debug.Log("ENTRO A JUMP");
            _rb.AddForce(Vector2.up * _jumpForce);
            _isGrounded = false;
        } //char

        public void KeyInput(char _key)
        {
            if ( _isGrounded && (_key == 'W')) { Jump(); }
        }
        public void HorizontalMove(float _horizontal)
        {
            _rb.velocity = new Vector2(_horizontal, _rb.velocity.y);
        } //char
        public void InsideCloudMovement(float _horizontal, float _vertical, GameObject _cloudChar) //cloudChar
        {
            Vector2 direction = new Vector2(_horizontal, _vertical).normalized;
            _cloudChar.transform.Translate(direction * _coreMoveSpeed * Time.deltaTime);
        }
        public void Movement(float _horizontal, float _vertical) //general
        {
            if (_insideCloud && _cloudChar is not null) 
            {
                InsideCloudMovement(_horizontal, _vertical, _cloudChar);
            }
            else
            {
                HorizontalMove(_horizontal);
            }
        }
        public void CheckTriggerEnter(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Ground"))
            {
                StartCoroutine(SetGroundedWithDelay());
                Debug.Log("CHEKEANDO GROUND");
            }
            if (collider.gameObject.CompareTag("CloudBlock"))
            {
                if (!_insideCloud) { GoInsideCloud(collider); }
                Debug.Log("CHEKEANDO CLOUD");
            }
        } //general
        public void GoInsideCloud(Collider2D collider)
        {
            _insideCloud = true;
            // Obtener el centro del collider del objeto que entró en el trigger
            Vector2 localCenter = collider.offset;
            Vector2 worldCenter = (Vector2)collider.transform.position + localCenter;
            // Instanciar el prefab en el centro del collider
            GameObject _newCloudChar = Instantiate(_cloudChar, worldCenter, Quaternion.identity);
            _cloudChar = _newCloudChar;
            deactivateCharacter(_char);
        } //cloudChar
        public void deactivateCharacter(GameObject _char)
        {
            MakeSelfVisible(_char, false);
            Debug.Log("DESACTIVANDO PJ");
            _char.transform.position = Vector2.zero;
            _insideCloud = true;

        } //char
        public void activateCharacter()
        {
            MakeSelfVisible(_char, true);
            _char.transform.position = _cloudChar.transform.position;
        } //char
        void MakeSelfVisible(GameObject _char, bool isVisible)
        {
            SpriteRenderer spriteRenderer = _char.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = isVisible; // Hacer el sprite invisible
            }
        } //char
        private IEnumerator SetGroundedWithDelay()
        {
            yield return new WaitForSeconds(0.05f); // Retardo pequeño para evitar el "bug"
            _isGrounded = true;
        } //char

        public void CheckTriggerExit(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Ground"))
            {
                _isGrounded = false;
            }
            if (collider.gameObject.CompareTag("CloudBlock"))
            {
                _insideCloud = false;
                activateCharacter();
            }
        }*/
