using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help {

    public string cmd = "USR/JO_HOPPER > ";
    public Dictionary<string, string> msg = new Dictionary<string, string>(){
        {"help", "You know what it means!"},
        {"ls", "List directory contents"},
        {"open [file]", "Open the given file [filename.extension]. All files have unique names."}
    };
    
    public Help(string user_cmd) {

        cmd += user_cmd; 
    }
}
