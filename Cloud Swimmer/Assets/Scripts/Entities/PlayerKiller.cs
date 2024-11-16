using Assets.Scripts.Controlers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("OutsideCharacter"))
        {
            GameManager.Instance.RespawnPlayer();
            
        }
    }
}
