using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D script;

    float horizontalAxis = 0f; // x input
    public float walkSpeed = 40f; // default 

    bool jump = false;
    bool crouch = false;
    bool run = false;
    bool catchBug = false;


    void Update()
    {

        horizontalAxis = Input.GetAxisRaw("Horizontal") * walkSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
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

  

    void FixedUpdate()
    {
        script.Move(horizontalAxis * Time.fixedDeltaTime, crouch, jump, run, catchBug);
        jump = false;
        catchBug = false;
    }
    
    
}