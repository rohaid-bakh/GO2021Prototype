using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ls {
    
    public string cmd = "USR/JO_HOPPER > ";
    public Node<string> msg = new Node<string>(
        "Root", 
        new List<Node<string>>() {
            new Node<string> (
                "Parent1",
                new List<Node<string>>(){
                    new Node<string> ("Child1"),
                    new Node<string> ("Child2"),
                    new Node<string> ("Child3")
                }
            ),
            new Node<string> (
                "Parent2",
                new List<Node<string>>(){
                    new Node<string> ("Child1"),
                    new Node<string> ("Child2"),
                    new Node<string> ("Child3")
                }
            )
        }
    );

    public Ls(string user_cmd) {

        cmd += user_cmd; 
    }
}
