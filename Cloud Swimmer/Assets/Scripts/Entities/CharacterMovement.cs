using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CloudSwimmer.Entities
{
    public class CharacterMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;       // Velocidad de movimiento
        public float jumpForce = 10f;      // Fuerza de salto

        private Rigidbody2D _rb;
        private bool _isGrounded;
        private float _horizontal;
        private bool _isJumping;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Jump()
        {
            if (_isGrounded) {
                _rb.AddForce(Vector2.up * jumpForce);
                _isGrounded = false;
            } 
        }
        public void HorizontalMove(float _horizontal)
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
        
    }
}
/*public float moveSpeed = 5f;       // Velocidad de movimiento
        public float jumpForce = 10f;      // Fuerza de salto

        private Rigidbody2D _rb;
        private bool _isGrounded;
        private float _horizontal;
        private bool _isJumping;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Jump()
        {
            _rb.AddForce(Vector2.up * jumpForce);
        }

        void Update()
        {
            // Movimiento horizontal
            _horizontal = Input.GetAxisRaw("Horizontal");
            if (Input.GetKeyDown(KeyCode.W) && _isGrounded)
            {
                //_rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                Jump();
                _isGrounded = false;
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Verifica si el objeto tocado es el suelo y previene múltiples saltos
            if (other.gameObject.CompareTag("Ground"))
            {
                StartCoroutine(SetGroundedWithDelay());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // El personaje deja el suelo solo si estaba tocándolo antes de saltar
            if (other.gameObject.CompareTag("Ground"))
            {
                _isGrounded = false;
            }
        }
        private IEnumerator SetGroundedWithDelay()
        {
            yield return new WaitForSeconds(0.05f); // Retardo pequeño para evitar el "bug"
            _isGrounded = true;
        }
        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_horizontal, _rb.velocity.y);
        }*/
