using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudSwimmer.Interfaces;
using CloudSwimmer.Interface;

namespace CloudSwimmer.Entities {
    public class CloudBlockCreator : MonoBehaviour, IBlockCreator 
    {
        //This class is a singleton
        private static CloudBlockCreator _instance;
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


        public GameObject _cloud;
        public Transform _cloudContainer;
        private Vector2 _spawnPosition;
        
        public float _minDistance = 0.1f;
        public int _cloudQuantity;

        void Start()
        {
            _cloudQuantity = 5;
            _spawnPosition = Input.mousePosition;
        }

        public void IncreaseCloudQuantity(int quantity)
        {
            _cloudQuantity += quantity;
        }
        public void DebugPosition(Vector2 mousePosition)
        {
            Debug.Log("Posición del mouse: " + mousePosition + "Powered by Cloud Creator :)");
        }

        public void CreateBlock(Vector2 mousePosition)
        {
            if (Vector2.Distance(_spawnPosition, mousePosition) >= _minDistance && (_cloudQuantity > 0))
            {
                _cloud.transform.position = mousePosition;
                //Instantiate(_cloud, parent:_cloudContainer);
                Debug.Log("cloudContainer: " + _cloudContainer);
                GameObject newCloud = Instantiate(_cloud, mousePosition, Quaternion.identity, _cloudContainer);
                newCloud.transform.SetParent(_cloudContainer);
                _spawnPosition = mousePosition;
                _cloudQuantity -= 1;
            }
            else
            {
                //_cloud.transform.position = mousePosition;
                ////Instantiate(_cloud, parent:_cloudContainer);
                //Debug.Log("cloudContainer: " + _cloudContainer);
                //GameObject newCloud = Instantiate(_cloud, mousePosition, Quaternion.identity, _cloudContainer);
                //newCloud.transform.SetParent(_cloudContainer);
                //_spawnPosition = mousePosition;
            }
        }
        void Update()
        {
            
        }
    }
}

