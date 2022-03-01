using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform groundCheck;
    [SerializeField] bool movingRight = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyPatrol();

    }

    private void EnemyPatrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D ray = Physics2D.Raycast(groundCheck.position, Vector2.down, 1f);
        if (ray.collider == false)
        {
            if (movingRight)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                movingRight = false;
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                movingRight = true;
            }
        }
    }
}
