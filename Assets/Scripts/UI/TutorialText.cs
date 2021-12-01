using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialText : MonoBehaviour
{
    [SerializeField]
    private GameObject Bug;
    [SerializeField] private GameObject[] textDisplay;

    private int commandCount = 0;
    private bool textSwitch = false;

    public float waitTime = 2f;

    public float currentTime = 0f;

    void Awake(){
    currentTime = waitTime;
    Bug.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        if(currentTime < 0){
        for (int i = 0 ; i < textDisplay.Length ; i++) {
            
            if (i == commandCount){
            textDisplay[i].SetActive(true);
            } else {
                textDisplay[i].SetActive(false);
            }
    
        }
        }
        if (commandCount == 0)
        {

            if (Input.GetAxisRaw("Horizontal") > 0.1 || Input.GetAxisRaw("Horizontal") < -.01)
            {
                commandCount++;
                currentTime = waitTime;
            }
        }
        else if (commandCount == 1)
        {
            if (Input.GetButtonDown("Jump"))
            {
                commandCount++;
                currentTime = waitTime;
            }
        }
        else if (commandCount == 2)
        {
            if (!textSwitch)
            {
                textSwitch = !textSwitch;
            }
            if (Input.GetButtonDown("Crouch"))
            {
                commandCount++;
                currentTime = waitTime;
            }
        } else if (commandCount == 3) {
            if (currentTime < 0) {
                
                 Bug.GetComponent<BoxCollider2D>().enabled = true;
            }

        }

    }

}
