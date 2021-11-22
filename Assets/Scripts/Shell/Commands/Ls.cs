using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ls {
    
    public string cmd = "USR/JO_HOPPER > ";
    public Node<string> msg = new Node<string>(
        "Root", 
        new List<Node<string>>() {
            new Node<string> (
                "Parent 1",
                new List<Node<string>>(){
                    new Node<string> ("Child 1"),
                    new Node<string> ("Child 2"),
                    new Node<string> ("Child 3")
                }
            ),
            new Node<string> (
                "Parent 2",
                new List<Node<string>>(){
                    new Node<string> ("Child 1"),
                    new Node<string> ("Child 2"),
                    new Node<string> ("Child 3")
                }
            )
        }
    );

    public Ls(string user_cmd) {

        cmd += user_cmd; 
    }
}
