using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudSwimmer.Interfaces;
using CloudSwimmer.Interface;

namespace CloudSwimmer.Entities {
    public class CloudBlockCreator : MonoBehaviour, IBlockCreator 
    {
        private static CloudBlockCreator _instance;

        public GameObject _cloud;
        private List<GameObject> _clouds;
        //public SpriteRenderer spriteTarget;

        private Vector3 mousePosition;

        //this concrete factory its implemented as a singleton
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

        /*public IBlock createBlock()
        { 

            // Crea un nuevo GameObject
            GameObject spriteObject = new GameObject("CloudBlock");

            // Añade un componente SpriteRenderer
            SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();

            // Asigna el sprite al SpriteRenderer
           // spriteRenderer.sprite = spriteToAssign;

            // (Opcional) Ajusta la posición del GameObject
            spriteObject.transform.position = new Vector3(0, 0, 0);
            spriteObject.name = "Juan";
            Instantiate(spriteObject);
        }*/
        void Update()
        {

        }

        
    }
}

