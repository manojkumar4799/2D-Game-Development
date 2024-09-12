using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        Debug.Log("press r to run and c to crouch");
    }

    
    void Update()
    {
        PlayerRunAnimation();

        PlayerCrouchAnimation();

    }

    private void PlayerCrouchAnimation()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerAnimator.SetBool("crouch", true);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            playerAnimator.SetBool("crouch", false);
        }
    }

    private void PlayerRunAnimation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerAnimator.SetFloat("speed", 0.6f);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            playerAnimator.SetFloat("speed", 0f);
        }
    }
}
