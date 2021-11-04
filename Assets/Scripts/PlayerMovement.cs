using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D script;

    float horizontalAxis = 0f;
    public float runSpeed = 40f;

    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        
        horizontalAxis = Input.GetAxisRaw("Horizontal") * runSpeed ;

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }
        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
    } 

    void FixedUpdate() {
        script.Move(horizontalAxis * Time.fixedDeltaTime, crouch, jump, false);
        jump = false;
    }
}
