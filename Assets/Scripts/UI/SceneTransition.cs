using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneTransition : MonoBehaviour
{
   public Animator animationTransition;
    public Animator musicTransition;
   
   private Scene scene;

   public float transitionTime = 1f;
        [SerializeField] private PauseMenu pauseScript;
     [SerializeField] private Button pauseButton;
     
   void OnTriggerEnter2D(Collider2D col){
      pauseScript.enabled = false;
      pauseButton.enabled = false;
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
