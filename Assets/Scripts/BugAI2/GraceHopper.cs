using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraceHopper : MonoBehaviour
{

    [SerializeField] public BugsCaught script;
    private bool activate = false;

    // Update is called once per frame
    void Update()
    {
        if (!activate && script.bugsCaught == 12) {
             StartCoroutine(ShowGraceHopper());
             activate = false;
        }

        
    }


      IEnumerator ShowGraceHopper(){
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        
    }
}
