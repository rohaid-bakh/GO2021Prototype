using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D script;
    public Animator animator;

    float horizontalAxis = 0f; // x input
    public float walkSpeed = 40f; // default 

    bool jump = false;
    bool crouch = false;
    bool run = false;
    bool catchBug = false;


    void Update()
    {
        if (!PauseMenu.isPaused) {

        horizontalAxis = Input.GetAxisRaw("Horizontal") * walkSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalAxis));

        
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        if(!animator.GetBool("Crouch")){
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jump", true);
        }
        }
        if (Input.GetButtonDown("Run"))
        {
            run = true;
        }
        else if (Input.GetButtonUp("Run"))
        {
            run = false;
        }

        if (Input.GetButtonDown("Catch"))
        {
            catchBug = true;
        }

        }

    }

    public void onLanding() {
        animator.SetBool("Jump", false);
    }

    public void onCrouch(bool onCrouch){
        animator.SetBool("Crouch", onCrouch);
    }
  

    void FixedUpdate()
    {
        script.Move(horizontalAxis * Time.fixedDeltaTime, crouch, jump, run, catchBug);
        jump = false;
        catchBug = false;
    }
    
    
}