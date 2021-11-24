using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class BugsCaught : MonoBehaviour
{
    [SerializeField] private GameObject bugs;
    [SerializeField] private Text bugText;
    private Scene scene;
    public int bugsCaught = 0;

    private bool m_endScene;
    [SerializeField] private GameObject TextManager;
    [SerializeField] private TextMeshProUGUI dialougeDisplay;
    [SerializeField] private GameObject NextLevel;
    [SerializeField] private string[] dialougeText;
    
    private int index = 0;
    [SerializeField] private float typingSpeed;

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

        if (bugsCaught == bugs.transform.childCount && !m_endScene) {
          m_endScene = true;
          endScene();

        }

    }

    void endScene(){
        scene = SceneManager.GetActiveScene();
        TextManager.SetActive(true);
        if (scene.buildIndex == 1){
            StartCoroutine(Type());
            NextLevel.SetActive(true);
        }

        if (scene.buildIndex == 2){
            //have Grasshooper show up 
        }

    }

    IEnumerator Type() {
       
        foreach(char letter in dialougeText[index].ToCharArray()){
            dialougeDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        
    }
}