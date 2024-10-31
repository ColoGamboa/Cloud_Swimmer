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

        private IBlockCreator blockCreator;

        private Vector3 mousePosition;

        
        void Start()
        {
            blockCreator = CloudBlockCreator.Instance;
        }


        private void leftClickPressDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition; // Obtiene la posición del mouse en la pantalla

                // Convierte la posición de la pantalla a coordenadas del mundo
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mousePosition.z = 0; // Asegúrate de que la posición Z sea 0 en 2D

                // Aquí puedes usar la posición del mouse
                blockCreator.CreateBlock(mousePosition);
                blockCreator.DebugPosition(mousePosition);
            }
        }

        // Update is called once per frame
        void Update()
        {
            leftClickPressDown();
        }
    }
}

