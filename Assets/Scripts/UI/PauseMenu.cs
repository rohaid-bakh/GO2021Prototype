using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject helpMenuUI;
    [SerializeField] private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)){
            if (isPaused) {
             ResumeGame();
                } else {
            PauseGame();
            }
        }

        
    }

    public void PauseGame(){

        pauseMenuUI.SetActive(true);
        setPause(true);
        Time.timeScale = 0f;
        audio.volume *= .5f;

    }

    public void ResumeGame(){
         pauseMenuUI.SetActive(false);
         setPause(false);
         Time.timeScale = 1f;
         audio.volume *= 2f;
    }

    public void LoadHelpMenu() {
        pauseMenuUI.SetActive(false);
        helpMenuUI.SetActive(true);

    }
    public void QuitQame(){
        Debug.Log("Quit");
        Application.Quit();
    }

    public void setPause(bool pause){
        isPaused = pause;
    }

}
