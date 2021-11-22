using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invalid {

    public string cmd = "USR/JO_HOPPER > ";
    public string msg = "jeez: command not found: ";

    public Invalid(string user_cmd) {

        cmd += user_cmd;
        msg += user_cmd; 
    }
}
