using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open {

    public string cmd = "USR/JO_HOPPER > ";

    public Open(string user_cmd) {

        cmd += user_cmd;
    }
}
