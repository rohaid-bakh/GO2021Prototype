using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellManager : MonoBehaviour
{
    public InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField.onSubmit.AddListener(delegate { Run(inputField.text); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Run(string cmd) {
        
        if (cmd == "help") {
            
            RunCmd_Help();

        } else if (cmd == "ls") {

            RunCmd_List();
        }
    }

    void RunCmd_Help() {

    }

    void RunCmd_List() {

    }
}
