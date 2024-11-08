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

            // Detectar arrastre mientras el clic está presionado
            if (isDragging)
            {
                Vector2 currentMousePosition = Input.mousePosition;

                // Comprobar si el mouse se ha movido
                if (currentMousePosition != lastMousePosition)
                {
                    Debug.Log("Arrastrando... posición del mouse: " + currentMousePosition);

                    // Aquí puedes añadir la lógica para hacer algo mientras se arrastra
                    // ...

                    // Actualizar la última posición del mouse
                    lastMousePosition = currentMousePosition;
                }
            }
        }


        //private void leftClickPressDown()
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Vector3 mousePosition = Input.mousePosition; // Obtiene la posición del mouse en la pantalla

        //        // Convierte la posición de la pantalla a coordenadas del mundo
        //        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //        mousePosition.z = 0; // Asegúrate de que la posición Z sea 0 en 2D

        //        // Aquí puedes usar la posición del mouse
        //        blockCreator.CreateBlock(mousePosition);
        //        blockCreator.DebugPosition(mousePosition);
        //    }
        //}

        // Update is called once per frame

    }
}

