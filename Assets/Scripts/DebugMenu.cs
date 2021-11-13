using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DebugMenu : MonoBehaviour
{
    //This file serves as a debug menu for testing.
    // Press G to switch between two different maps

    //TO DO: Allow for switching between more than 2 maps
    [SerializeField] private UnityEvent m_arenaSwitchInvoke;
    [SerializeField] private GameObject runArena;
    [SerializeField] private GameObject crouchArena;

    [SerializeField] private CharacterController2D script;
   
   
   void Start() {

     
        if (m_arenaSwitchInvoke == null){
            m_arenaSwitchInvoke = new UnityEvent();
        }


    }


    // Update is called once per frame
    void Update()
    {
          if (Input.GetButtonDown("Switch")) {
           m_arenaSwitchInvoke.Invoke();
        }
        
    }

    public void mapChange(){
            runArena.SetActive(!runArena.activeSelf);
            crouchArena.SetActive(!crouchArena.activeSelf);
    }

    void OnDrawGizmosSelected(){
            if (script.catchCheck == null) {
                return;
            }
            Gizmos.DrawWireSphere(script.catchCheck.position, script.catchRange);
    }
}
