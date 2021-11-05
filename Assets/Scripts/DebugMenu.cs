using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DebugMenu : MonoBehaviour
{
    [SerializeField] private UnityEvent m_arenaSwitchInvoke;
    [SerializeField] private GameObject runArena;
    [SerializeField] private GameObject crouchArena;

    // Start is called before the first frame update
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
}
