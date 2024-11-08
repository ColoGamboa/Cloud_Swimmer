using CloudSwimmer.Entities;
using CloudSwimmer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CloudSwimmer.Controllers
{
    public class CloudController : MonoBehaviour
    {

        //revisar esta clase para que funcione con un patron observer (hay q implementar los
        //controladores especificos suscritos a este controlador)

        private IBlockCreator blockCreator;
        void Start()
        {
            blockCreator = CloudBlockCreator.Instance;
        }


        private Vector2 lastMousePosition;
        private Vector2 currentMousePosition;
        private bool isDragging;

        void leftClickPressDown() {
            isDragging = true;
            //lastMousePosition = Input.mousePosition;
            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Inicio de arrastre");
            blockCreator.CreateBlock(lastMousePosition);
        }
        void leftClickIsDragged()
        {
            //Vector2 currentMousePosition = Input.mousePosition;
            currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Comprobar si el mouse se ha movido
            if (currentMousePosition != lastMousePosition)
            {
                //Debug.Log("Arrastrando... posición del mouse: " + currentMousePosition);
                blockCreator.DebugPosition(currentMousePosition);
                blockCreator.CreateBlock(currentMousePosition);
                // Actualizar la última posición del mouse
                lastMousePosition = currentMousePosition;
            }
        }
        void Update()
        {
            // Detectar cuando el usuario presiona el clic izquierdo (inicio de arrastre)
            if (Input.GetMouseButtonDown(0))
            {
                leftClickPressDown();
            }

            // Detectar cuando el usuario suelta el clic (fin del arrastre)
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                Debug.Log("Fin del arrastre");
            }

            // Detectar arrastre mientras el clic está presionado
            if (isDragging)
            {
                leftClickIsDragged();
            }

            // Update is called once per frame
        }
    }
}
