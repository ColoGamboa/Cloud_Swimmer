using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudSwimmer.Entities;
using CloudSwimmer.Interfaces;

namespace CloudSwimmer.Controllers
{
    public class InputController : MonoBehaviour
    {

        //revisar esta clase para que funcione con un patron observer (hay q implementar los
        //controladores especificos suscritos a este controlador)

        //private IBlockCreator blockCreator;
        //private Vector3 mousePosition;


        private Vector2 lastMousePosition;
        private bool isDragging;

        void Update()
        {
            // Detectar cuando el usuario presiona el clic izquierdo (inicio de arrastre)
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                lastMousePosition = Input.mousePosition;
                Debug.Log("Inicio de arrastre");
            }

            // Detectar cuando el usuario suelta el clic (fin del arrastre)
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                Debug.Log("Fin del arrastre");
            }

            // Detectar arrastre mientras el clic est� presionado
            if (isDragging)
            {
                Vector2 currentMousePosition = Input.mousePosition;

                // Comprobar si el mouse se ha movido
                if (currentMousePosition != lastMousePosition)
                {
                    Debug.Log("Arrastrando... posici�n del mouse: " + currentMousePosition);

                    // Aqu� puedes a�adir la l�gica para hacer algo mientras se arrastra
                    // ...

                    // Actualizar la �ltima posici�n del mouse
                    lastMousePosition = currentMousePosition;
                }
            }
        }


        //private void leftClickPressDown()
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Vector3 mousePosition = Input.mousePosition; // Obtiene la posici�n del mouse en la pantalla

        //        // Convierte la posici�n de la pantalla a coordenadas del mundo
        //        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //        mousePosition.z = 0; // Aseg�rate de que la posici�n Z sea 0 en 2D

        //        // Aqu� puedes usar la posici�n del mouse
        //        blockCreator.CreateBlock(mousePosition);
        //        blockCreator.DebugPosition(mousePosition);
        //    }
        //}

        // Update is called once per frame

    }
}

