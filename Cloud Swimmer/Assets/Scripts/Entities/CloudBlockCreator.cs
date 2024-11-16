using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudSwimmer.Interfaces;
using CloudSwimmer.Interface;
using Assets.Scripts.Controlers;
using Unity.VisualScripting;

namespace CloudSwimmer.Entities {
    public class CloudBlockCreator : MonoBehaviour, IBlockCreator 
    {
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
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        public GameObject _cloud;
        public Transform _cloudContainer;
        private Vector2 _spawnPosition;

        [SerializeField] private float _minDistance;
        [SerializeField] private float _delay;
        [SerializeField] private int _cloudQuantity;

        void Start()
        {
            _spawnPosition = Input.mousePosition;
            _minDistance = 0.70f;
            _delay = 0.1f;
        }
        public void DebugPosition(Vector2 mousePosition)
        {
            Debug.Log("Posición del mouse: " + mousePosition + "Powered by Cloud Creator :)");
        }
        public void CreateBlock(Vector2 mousePosition)
        {
            if (Vector2.Distance(_spawnPosition, mousePosition) >= _minDistance && (_cloudQuantity > 0))
            {
                Delay(_delay);
                _cloud.transform.position = mousePosition;
                GameObject newCloud = Instantiate(_cloud, mousePosition, Quaternion.identity, _cloudContainer);
                newCloud.transform.SetParent(_cloudContainer);
                _spawnPosition = mousePosition;
                GameManager.Instance.IncreaseCloudCount(-1);
            }
        }
        IEnumerator Delay(float delay)
        {
            yield return new WaitForSeconds(delay);
            
        }
        public void DestroyBlock()
        {
            foreach (Transform child in _cloudContainer)
            {
                Destroy(child.gameObject);
                GameManager.Instance.IncreaseCloudCount(1);
            }
        }
        void Update()
        {
            _cloudQuantity = GameManager.Instance.GetCloudCount();
        }
    }
}

