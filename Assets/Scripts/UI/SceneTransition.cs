using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
   public Animator animationTransition;
    public Animator musicTransition;
   
   private Scene scene;

   public float transitionTime = 1f;
   void OnTriggerEnter2D(Collider2D col){
    if (col.name == "MC"){
       SceneAnimation();
    }
   }

   public void SceneAnimation(){
      StartCoroutine(LoadAnimation());
   }
   IEnumerator LoadAnimation() {
      animationTransition.SetTrigger("Start");
      musicTransition.SetTrigger("Start");
      yield return new WaitForSeconds(3f);
      scene = SceneManager.GetActiveScene();
      SceneManager.LoadScene(scene.buildIndex+1, LoadSceneMode.Single);
      //Wait
      //Load Scene
   }
}
