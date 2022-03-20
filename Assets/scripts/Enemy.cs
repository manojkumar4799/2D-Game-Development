using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform groundCheck;
    [SerializeField] bool movingRight = true;
    Animator enemyAnimator;
    public float enemyHealth;
    
    
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        
    }

    
    void Update()
    {
        EnemyPatrol();
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            collision.gameObject.GetComponent<PlayerController>().PlayerDamage();
            enemyAnimator.SetTrigger("Attack");

        }
        if(collision.gameObject.CompareTag("Power"))
        {
            TakeDamage();
        }
    }



    private void TakeDamage()
    {
        enemyHealth--;
        if (enemyHealth <= 0)
        {
            enemyAnimator.SetTrigger("Death");
            Invoke("EnemyDeath",1f);
        }
    }

    public void EnemyDeath()
    {
        gameObject.SetActive(false);
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
