using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionControl : MonoBehaviour
{
    [SerializeField] int sceneNum;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NextScene")
        {
            SceneManager.LoadScene(sceneNum++);
        }
    }
}
