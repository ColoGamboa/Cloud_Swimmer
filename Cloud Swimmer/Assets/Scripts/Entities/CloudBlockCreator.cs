using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudSwimmer.Interfaces;
using CloudSwimmer.Interface;

namespace CloudSwimmer.Entities {
    public class CloudBlockCreator : MonoBehaviour, IBlockCreator 
    {
        private static CloudBlockCreator _instance;
        //This class is a singleton
        public GameObject _cloud;
        private List<GameObject> _clouds;
        //public SpriteRenderer spriteTarget;

        private Vector2 lastSpawnPosition;

        public static CloudBlockCreator Instance
        {
            get {
                if (_instance == null)
                {
                    GameObject singletonCloudBlockCreator = new GameObject();
                    _instance = singletonCloudBlockCreator.AddComponent<CloudBlockCreator>();
                    singletonCloudBlockCreator.name = typeof(CloudBlockCreator).Name + " (Singleton)";

                    // No destruir el objeto al cargar nuevas escenas
                    DontDestroyOnLoad(singletonCloudBlockCreator);
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


        void Start()
        {
            //Instantiate(_cloud);
            //var juanObjectSprite = Instantiate(spriteTarget);
            //juanObjectSprite.color = Color.red;
        }

        public void DebugPosition(Vector3 mousePosition)
        {
            Debug.Log("Posición del mouse: " + mousePosition + "Powered by Cloud Creator :)");
        }

        public void CreateBlock(Vector3 mousePosition)
        {
            //GameObject newCloud = new GameObject("CloudBlock");
            //newCloud = _cloud;
            _cloud.transform.position = mousePosition;

            Instantiate(_cloud);
        }

        private bool isDragging;
        private float minDistance = 0.5f;

        public void createBlockRow(Vector3 mousePosition)
        {

            isDragging = true;
            lastSpawnPosition = mousePosition;
            //Instantiate(prefabA, lastSpawnPosition, Quaternion.identity); // Instanciar el primero
            CreateBlock(mousePosition);

            // Detectar cuando el usuario suelta el clic (fin del arrastre)
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            // Instanciar durante el arrastre si el mouse se ha movido suficiente distancia
            if (isDragging)
            {
                //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(lastSpawnPosition, mousePosition) >= minDistance)
                {
                    //Instantiate(prefabA, mousePosition, Quaternion.identity);
                    CreateBlock(mousePosition);
                    lastSpawnPosition = mousePosition; // Actualizar la última posición
                }
            }
        }
        void Update()
        {
            
        }

        
    }
}

