using Assets.Scripts.Interfaces;
using CloudSwimmer.Entities;
using System;
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
        public void Init(Collider2D collider)
        {           
            // Obtener el centro del collider del objeto que entró en el trigger
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
            throw new NotImplementedException();
        }

        public void CheckTriggerExit(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Cloud"))
            {
                SpriteRenderer _spriteRenderer = _char.GetComponent<SpriteRenderer>();
                _spriteRenderer.color = Color.red;
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Verifica si el objeto que entra tiene el tag "CloudBlock"
            if (other.CompareTag("CloudBlock"))
            {
                Debug.Log("ENTRANDO EN CLOUD");
                _blocksInside++; // Incrementa el contador
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            // Esto podría ser útil si necesitas realizar acciones mientras está en colisión
            if (other.CompareTag("CloudBlock"))
            {
                Debug.Log("Permaneciendo dentro de un CloudBlock.");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // Verifica si el objeto que sale tiene el tag "CloudBlock"
            if (other.CompareTag("CloudBlock"))
            {
                _blocksInside--; // Decrementa el contador
                CheckTriggerExit(other);
                Debug.Log("ENTRANDO EN CLOUD");
            }
        }
        private void deactivateCharacter(GameObject _char)
        {
            Debug.Log("DESACTIVANDO CHAR");
            MakeSelfVisible(_char, false);
            _char.transform.position = Vector2.zero;
        }

        public void KeyInput(char _key)
        {
            throw new NotImplementedException();
        }

        public void Move(float _horizontal, float _vertical)
        {
            Debug.Log("CLOUD MOVE");
            Vector2 direction = new Vector2(_horizontal, _vertical).normalized;
            Debug.Log("Dirección: " + direction + ", Velocidad: " + _speed + ", DeltaTime: " + Time.deltaTime);
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
    }
}
