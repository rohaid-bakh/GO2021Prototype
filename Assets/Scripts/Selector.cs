using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public Text file;
    public string fileName;

    private bool isHoverering = false;
    private bool isOpened = false;

    public GameObject alertBox;
    public GameObject glitch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpened && isHoverering && Input.GetMouseButton(0)) {
            alertBox.SetActive(true);
            glitch.SetActive(true);
            isOpened = true;
        }
    }

    private void OnMouseOver() {
        if (!isOpened && file.text.Equals(fileName)) {
            gameObject.GetComponent<Renderer>().enabled = true;
            isHoverering = true;
        }
    }

    private void OnMouseExit() {
        gameObject.GetComponent<Renderer>().enabled = false;
        isHoverering = false;
    }
}
