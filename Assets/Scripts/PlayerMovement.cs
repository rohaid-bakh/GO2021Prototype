using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D script;

    float horizontalAxis = 0f;
    public float runSpeed = 40f;

    bool jump = false;


    // Update is called once per frame
    void Update()
    {
        
        horizontalAxis = Input.GetAxisRaw("Horizontal") * runSpeed ;

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }
    } 

    void FixedUpdate() {
        script.Move(horizontalAxis * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
