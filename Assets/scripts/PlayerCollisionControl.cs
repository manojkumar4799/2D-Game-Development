using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionControl : MonoBehaviour
{
    
    [SerializeField] string sceneName;
    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NextScene")
        {
            SceneManager.LoadScene(sceneName);
        }

        if (collision.gameObject.tag == "DeathCollider")
        {
            playerController.PlayerDeath();
            playerController.enabled = false;
            Destroy(gameObject, 2);
           
           
        }

        
    }
}
