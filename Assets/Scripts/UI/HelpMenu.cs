using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject helpMenuUI;
    [SerializeField] private AudioSource audio;
    [SerializeField] private PauseMenu pauseMenu;
    
    public void returnToPause(){
        helpMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void returnToGame(){
        helpMenuUI.SetActive(false);
        pauseMenu.ResumeGame();
    }
}
