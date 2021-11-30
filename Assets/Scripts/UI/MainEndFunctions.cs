using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainEndFunctions : MonoBehaviour
{

    [SerializeField] private GameObject helpMenuUI;
      [SerializeField] private GameObject creditsMenuUI;
   public void MainMenu(){
       SceneManager.LoadScene(0, LoadSceneMode.Single);
   }

  public void Play(){
      SceneManager.LoadScene(1, LoadSceneMode.Single);
  }

  public void LoadHelp(){
      if (helpMenuUI != null) {
          helpMenuUI.SetActive(true);
      }
  }

  public void CloseHelp(){
      if (helpMenuUI != null){
          helpMenuUI.SetActive(false);
      }
  }

  public void ShowCredits(){
      if (creditsMenuUI != null){
          creditsMenuUI.SetActive(true);
      }
  }

  public void HideCredits(){
       if (creditsMenuUI != null){
          creditsMenuUI.SetActive(false);
      }
  }
}
