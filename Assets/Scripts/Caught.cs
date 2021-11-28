using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caught : MonoBehaviour
{
  

    // Update is called once per frame
    public void Catch(Collider2D col) {
        //play animation before destroying
        // record the amount of bugs caught
        col.gameObject.SetActive(false);
        // deactivate
    }
}
