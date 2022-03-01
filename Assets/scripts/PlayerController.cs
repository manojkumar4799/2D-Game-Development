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
    public Rigidbody2D rb;
    public float speed;
    [SerializeField] float playerJump;

    [SerializeField] LayerMask groundlayer;
    [SerializeField] Transform groundcheckTansform;
    public float circleRadius;
    [SerializeField] bool isGrounded;
   // [SerializeField] Transform cameraTarnsform;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        fullBodyCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
       // cameraTarnsform = GetComponent<Transform>();
    }

    
    void Update()
    {
       // transform.position = cameraTarnsform.position;
        isGrounded = Physics2D.OverlapCircle(groundcheckTansform.position, circleRadius, groundlayer);
        RunPlayer();
        PlayerCrouch();
        PlayerJump();
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
        playerAnimator.SetTrigger("death");
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
