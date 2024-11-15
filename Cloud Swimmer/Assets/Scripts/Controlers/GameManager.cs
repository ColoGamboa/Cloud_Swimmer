using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controlers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        //[SerializeField] private int _cloudCount;
        private int _cloudCount;
        //public OnEndGame onEndGame;
        //public delegate void OnEndGame();

        //private int _score;

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
        }
        public int GetCloudCount()
        {
            return _cloudCount;
        }

        public void IncreaseCloudCount(int amount)
        {
            _cloudCount += amount;
            //Debug.Log($"Current Score: {_cloudCount}");
        }

        //private void EndGame()
        //{
        //    Debug.Log($"Game Finished");
        //    onEndGame?.Invoke();
        //}
    }
}

