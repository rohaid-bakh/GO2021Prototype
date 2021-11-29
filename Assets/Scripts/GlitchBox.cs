using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GlitchBox : MonoBehaviour
{
    public bool isHovering = false;
    public bool isClicked = false;

    public GameObject overlay;
    public GameObject glitch;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClicked && isHovering && Input.GetMouseButton(0)) {
            overlay.SetActive(true);
            glitch.SetActive(true);
            canvas.SetActive(true);
            isClicked = true;
            StartCoroutine(NextScene());
        }
    }

    private void OnMouseOver() {
        isHovering = true;
    }

    private void OnMouseExit() {
        isHovering = false;
    }

     IEnumerator NextScene() { 
         yield return new WaitForSeconds (3f);
         SceneManager.LoadScene(2, LoadSceneMode.Single);
     }
}
