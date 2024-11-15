using Assets.Scripts.Controlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class CloudPickUp:MonoBehaviour
    {      
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("OutsideCharacter"))
            {
                GameManager.Instance.IncreaseCloudCount(3);
                gameObject.SetActive(false);
            }
        }
    }
}
