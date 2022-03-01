using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinKey : MonoBehaviour
{
    [SerializeField] float keyPoints;
    [SerializeField] Score score;
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            score.ScoreDisPlay(keyPoints);
            Destroy(gameObject);
        }
    }
    void Update()
    {
        
    }
}
