using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerMovement : MonoBehaviour
{
  
    public CharacterController2D script;

    float horizontalAxis = 0f;
    public float walkSpeed = 40f;

    bool jump = false;
    bool crouch = false;
    bool run = false;

    // Update is called once per frame
        void Update()
    {
        
        horizontalAxis = Input.GetAxisRaw("Horizontal") * walkSpeed ;

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }
        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
        
        if (Input.GetButtonDown("Run")) {
            run = true;
        } else if (Input.GetButtonUp("Run")) {
            run = false;
        }

      
    } 

    void FixedUpdate() {
        script.Move(horizontalAxis * Time.fixedDeltaTime, crouch, jump, run);
        jump = false;
    }
}
