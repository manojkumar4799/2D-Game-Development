using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int bossHealth;
    public float attackAtDistance;
    public float shockAtDistance;
    float playerDistance = Mathf.Infinity;
    bool isProvoked = false;
    public GameObject enemyBeam;
    Animator bossAnimation;
    Transform playerPos;
    void Start()
    {
        playerPos = FindObjectOfType<PlayerController>().transform;
        bossAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        playerDistance = Vector2.Distance(playerPos.position, transform.position);
        if (playerDistance <= attackAtDistance)
        {
            isProvoked = true;
            if (playerDistance <= shockAtDistance)
            {
                bossAnimation.SetBool("Granade", false);
                bossAnimation.SetBool("Shock",true);
            }
            else
            {
                bossAnimation.SetBool("Granade", true);
                bossAnimation.SetBool("Shock", false);
            }       
        }
        else
        {
            isProvoked = false;
            bossAnimation.SetBool("Granade", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Power"))
        {
            TakeDamage();
        }
    }
    public void ShockAttack()
    {
        
        playerPos.GetComponent<PlayerController>().PlayerDamage();
    }

    public void BeamAttack()
    {
        
        GameObject beam= Instantiate(enemyBeam, transform.position, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
        Destroy(beam, 2f);
    }

    private void TakeDamage()
    {
        bossHealth--;
        if (bossHealth <= 0)
        {
            bossAnimation.SetTrigger("Death");
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void BossDeath()
    {
        Destroy(gameObject);
    }
}
