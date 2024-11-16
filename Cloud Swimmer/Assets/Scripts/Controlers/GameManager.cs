using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Assets.Scripts.Controlers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField] private int _cloudCount;
        [SerializeField] private GameObject[] _checkPoints;
        [SerializeField] private GameObject _currenCheckPoint;
        [SerializeField] private GameObject _player;
        //private int _checkPointIndex; 
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            _checkPoints = GameObject.FindGameObjectsWithTag("Checkpoints");    
        }
        public int GetCloudCount()
        {
            return _cloudCount;
        }
        public void IncreaseCloudCount(int amount)
        {
            _cloudCount += amount;
        }
        public void LastCheckPoint(GameObject _checkPoint)
        {
            _currenCheckPoint = _checkPoint;
        }
        public void RespawnPlayer()
        {
            _player.transform.position = _currenCheckPoint.transform.position;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RespawnPlayer();
            }
        }
    }
}

