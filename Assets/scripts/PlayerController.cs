using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [SerializeField] Animator playerAnimator;
    [SerializeField] BoxCollider2D fullBodyCollider;
    [SerializeField] SceneLoader sceneLoader;   
    public Rigidbody2D rb;  
    [SerializeField] LayerMask groundlayer;
    [SerializeField] Transform groundcheckTansform;
    PlayerHealth healthSystem;
    [SerializeField] GameObject DeathUIObject;
    [SerializeField] ParticleSystem deathVFX;
    [SerializeField] ParticleSystem winVFX;
    [SerializeField] GameObject attackPower;

    public float powerSpeed;
    public float powerTime;
    public int playerHealth;
    public float speed;
    [SerializeField] float playerJump;
    public float circleRadius;
    [SerializeField] bool isGrounded;
    bool doubleJump = false;
    [Header("collider settings")]
    public float yOffset;
    public float ySize;
    public float jumpYOffset;
    public float jumpYSize;
    public float crouchYOffset;
    public float crouchYSize;





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
        if (collision.gameObject.CompareTag("EnemyBeam"))
        {
            PlayerDamage();
            Destroy(collision.gameObject, 0.1f);
        }
        if (collision.gameObject.CompareTag("ChangeMusic"))
        {
            SoundManager.Instance.BackGroundMusic(Sound.BossMusic);
            Destroy(collision.gameObject, 0.1f);
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerAnimator.SetTrigger("Attack");
        }
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
            fullBodyCollider.offset = new Vector2(fullBodyCollider.offset.x, jumpYOffset);
            fullBodyCollider.size = new Vector2(fullBodyCollider.size.x, jumpYSize);
            doubleJump = true;
            SoundManager.Instance.PlaySound(Sound.jump);
            
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == false && doubleJump == true)
        {
            fullBodyCollider.offset = new Vector2(fullBodyCollider.offset.x, jumpYOffset);
            fullBodyCollider.size = new Vector2(fullBodyCollider.size.x, jumpYSize);
            playerAnimator.SetTrigger("jump");
            rb.velocity = new Vector2(0, playerJump);
            SoundManager.Instance.PlaySound(Sound.jump);
            doubleJump = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            fullBodyCollider.offset = new Vector2(fullBodyCollider.offset.x, yOffset);
            fullBodyCollider.size = new Vector2(fullBodyCollider.size.x, ySize);
          
        }
    }

    private void PlayerCrouch()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {

            fullBodyCollider.offset = new Vector2(fullBodyCollider.offset.x, crouchYOffset);
            fullBodyCollider.size = new Vector2(fullBodyCollider.size.x, crouchYSize);
            playerAnimator.SetBool("crouch", true);           
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {

            
            fullBodyCollider.offset = new Vector2(fullBodyCollider.offset.x, yOffset);
            fullBodyCollider.size = new Vector2(fullBodyCollider.size.x, ySize);
            playerAnimator.SetBool("crouch", false);
        }

    }

    public void PlayerAttack()
    {

        float powerDir = transform.localScale.x;
        GameObject power = Instantiate(attackPower, transform.position, Quaternion.identity) ;
        power.GetComponent<Rigidbody2D>().velocity = new Vector2(powerSpeed*powerDir, 0f);
        Destroy(power, powerTime);
    }

    public void PlayerDeath()
    {
        deathVFX.Play();
        bool isRunning = false;
        SoundManager.Instance.PlayerFootSteps(Sound.PlayerMove, isRunning);
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
