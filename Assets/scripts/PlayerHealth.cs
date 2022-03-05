using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject[] healthImage;
    PlayerController playercontoller;
    
    private void Start()
    {
        playercontoller = GetComponent<PlayerController>();
    }

    public void TakeDamage(int health)
    {
        health--;
        if (health <= 0)
        {            
            Destroy(healthImage[health].gameObject);
            playercontoller.PlayerDeath();            
        }
        else
        {            
            Destroy(healthImage[health].gameObject);            
        }       
       
    }

    
}
