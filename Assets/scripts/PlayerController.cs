using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [SerializeField] Animator playerAnimator;
    [SerializeField] BoxCollider2D fullBodyCollider;
    [SerializeField] BoxCollider2D bottomCollider;
    [SerializeField] BoxCollider2D topCollider;
    [SerializeField] SceneLoader sceneLoader;   
    public Rigidbody2D rb;  
    [SerializeField] LayerMask groundlayer;
    [SerializeField] Transform groundcheckTansform;
    PlayerHealth healthSystem;
    [SerializeField] GameObject DeathUIObject;
    [SerializeField] ParticleSystem deathVFX;
    [SerializeField] ParticleSystem winVFX;

    public int playerHealth;
    public float speed;
    [SerializeField] float playerJump;
    public float circleRadius;
    [SerializeField] bool isGrounded;


    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        fullBodyCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<PlayerHealth>();
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "NextScene")
        {
            this.enabled = false;
            winVFX.Play();
            Invoke("LevelComplete",2);
           
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerDamage();
        }


    }

    private void LevelComplete()
    {
        sceneLoader.LevelCompletePopup();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeathCollider")
        {
            PlayerDeath();
            this.enabled = false;          

        }
        
    }


    void Update()
    {
      
        isGrounded = Physics2D.OverlapCircle(groundcheckTansform.position, circleRadius, groundlayer);
        RunPlayer();
        PlayerCrouch();
        PlayerJump();
        
    }

    public void PlayerDamage()
    {
        if (playerHealth > 0)
        {
            healthSystem.TakeDamage(playerHealth);
            playerHealth--;
        }
                         
      
    }
    private void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&isGrounded)
        {
            playerAnimator.SetTrigger("jump");
            rb.velocity = new Vector2(0, playerJump);
            topCollider.enabled = true;
            fullBodyCollider.enabled = false;
            bottomCollider.enabled = false;
            SoundManager.Instance.PlaySound(Sound.jump);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            fullBodyCollider.enabled = true;
            bottomCollider.enabled = true;
            topCollider.enabled = false;
        }
    }

    private void PlayerCrouch()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            fullBodyCollider.enabled = false;
            topCollider.enabled = false;
            bottomCollider.enabled = true;
            playerAnimator.SetBool("crouch", true);           
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            fullBodyCollider.enabled = true;
            topCollider.enabled = false;
            bottomCollider.enabled = false;
            playerAnimator.SetBool("crouch", false);
        }

    }

    public void PlayerDeath()
    {
        deathVFX.Play();
        SoundManager.Instance.BackGroundMusic(Sound.PlayerDeath);
        this.enabled = false;
        playerAnimator.SetTrigger("death");
        Invoke("DeathUI", 2.2f);
        
    }
    private void DeathUI()
    {
        DeathUIObject.SetActive(true);
        Time.timeScale = 0f;
    }
    
   

    private void RunPlayer()
    {
        float playerXmovement = Input.GetAxisRaw("Horizontal");
        Vector2 playerPos = transform.position;
        playerPos.x = playerPos.x + playerXmovement * speed * Time.deltaTime;
        transform.position = playerPos;
        Vector3 scale = transform.localScale;
        playerAnimator.SetFloat("speed", Mathf.Abs(playerXmovement));
        playerAnimator.SetBool("jump", false);
        bool isRunning;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isRunning = true;
            SoundManager.Instance.PlayerFootSteps(Sound.PlayerMove, isRunning);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isRunning = false;
            SoundManager.Instance.PlayerFootSteps(Sound.PlayerMove, isRunning);
        }

        if (playerXmovement < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        } 
        if (playerXmovement > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
       
    }
}
