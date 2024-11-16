using CloudSwimmer.Entities;
using CloudSwimmer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CloudSwimmer.Controllers
{
    public class CloudController : MonoBehaviour
    {
        private IBlockCreator _blockCreator;
        void Start()
        {
            _blockCreator = CloudBlockCreator.Instance;
        }
        public Vector2 _lastMousePosition;
        public Vector2 _currentMousePosition;
        public bool _isDragging;
        void LeftClickPressDown()
        {
            _isDragging = true;
            _lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _blockCreator.CreateBlock(_lastMousePosition);
        }
        void LeftClickIsDragged()
        {
            _currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_currentMousePosition != _lastMousePosition)
            {
                _blockCreator.CreateBlock(_currentMousePosition);
                _lastMousePosition = _currentMousePosition;
            }
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                LeftClickPressDown();
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                Debug.Log("Fin del arrastre");
            }
            if (_isDragging)
            {
                LeftClickIsDragged();
            }
        }

    }
}
