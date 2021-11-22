using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BugsCaught : MonoBehaviour
{
    [SerializeField] private GameObject bugs;
    [SerializeField] private Text bugText;
   
    private int bugsCaught = 0;

    void Start()
    {
        bugText.text = "BUGS CAUGHT : " + bugsCaught + "/" + bugs.transform.childCount;
    }
    void Update()
    {
        int currentCatch = 0;
        for (int i = 0; i < bugs.transform.childCount; i++) {
            if (!bugs.transform.GetChild(i).gameObject.activeSelf){
                currentCatch++;
            }
        }

        if (currentCatch > bugsCaught) {
            bugsCaught = currentCatch;
        }

        bugText.text = "BUGS CAUGHT : " + bugsCaught + "/" + bugs.transform.childCount;

        if (currentCatch == bugs.transform.childCount) {
            //set scene transition
        }

    }
}
