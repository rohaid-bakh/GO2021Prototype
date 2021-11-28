using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
   private Scene scene;
   void OnTriggerEnter2D(Collider2D col){
    if (col.name == "MC"){
    scene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(scene.buildIndex+1, LoadSceneMode.Single);
    }
   }
}
