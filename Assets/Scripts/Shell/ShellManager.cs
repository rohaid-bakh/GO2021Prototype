using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellManager : MonoBehaviour
{
    public float minY;

    public InputField inputField;

    public Transform containerTransform;

    private Font kongtext;

    private int len;

    ShellUtils utils = new ShellUtils();

    private List<GameObject> logs = new List<GameObject>();

    private List<string> leafs = new List<string>();

    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        kongtext = Resources.Load("kongtext") as Font;
        inputField.onSubmit.AddListener(delegate { Run(inputField.text); });
        
        Ls ls0 = new Ls("ls");
        len = 9;
        DFS(ls0.msg, 0, new List<(string, bool, int)>());
        done = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (logs.Count > 0) {
            
            /* CAN BE OPTIMIZED! */
            var idx = BS_Under_MinY(logs);

            for (int i = idx; i >= 0; i--) {
                var temp = logs[idx];
                logs.Remove(temp);
                Destroy(temp);
            }
        }
    }

    void Run(string cmd) {

        var splitCmd = cmd.Split(" ");

        if (splitCmd.Length > 1) {

            if (splitCmd.Length == 2 && splitCmd[0].Equals("open")) {

                RunCmd_Open(splitCmd[1]);

            } else {

                InvalidCmd(cmd);
            }

        } else {
        
            if (cmd == "help") {
            
                RunCmd_Help();

            } else if (cmd == "ls") {

                RunCmd_List();

            } else {

                InvalidCmd(cmd);
            }
        }

        inputField.text = "";
        inputField.ActivateInputField();
    }

    void RunCmd_Open(string arg) {

        if (leafs.Contains(arg)) {

            AddNewLog("File " + arg + " opened!", 0, Color.white);
            
        } else {

            AddNewLog("File " + arg + " not found!", 0, Color.red);
        }

        Open open0 = new Open("open " + arg);
        AddNewLog(open0.cmd, 0, Color.black);

        // Open window
    }

    void RunCmd_Help() {

        Help help0 = new Help("help");

        foreach (KeyValuePair<string, string> log in help0.msg) {

            AddNewLog(log.Key + " --- " + log.Value, 0, Color.green);
        }

        AddNewLog(help0.cmd, 0, Color.black);
    }

    void RunCmd_List() {

        Ls ls0 = new Ls("ls");
        len = 9;
        
        DFS(ls0.msg, 0, new List<(string, bool, int)>());
        AddNewLog(ls0.cmd, 0, Color.black);
    }

    void InvalidCmd(string cmd) {

        Invalid invalid0 = new Invalid(cmd);

        AddNewLog(invalid0.msg, 0, Color.red);
        AddNewLog(invalid0.cmd, 0, Color.black);
    
    }

    int BS_Under_MinY(List<GameObject> logs) {
        
        int l = logs.Count - 1;
        int r = 0;

        if (l == r) {
            if (logs[l].transform.position.y < minY) {
                return l;   
            } else {
                return -1;
            }
        }

        while (l > r) {
            
            int mid = (l + r) / 2;
            
            if (logs[mid].transform.position.y < minY) {

                r = mid + 1;
            
            } else {

                l = mid;
            }
        }

        return l - 1;
    }

    void DFS(Node<string> node, int tabs, List<(string, bool, int)> nodeList) {
        
        len--;
        
        if (node.children == default(List<Node<string>>)){
            
            nodeList.Add((node.val, true, tabs));

            if (!done) {
            
                leafs.Add(node.val);
            
            } else {
            
                if (len == 0) {
                
                    nodeList.Reverse();
                
                    foreach ((string, bool, int) temp in nodeList) {
                        
                        if (temp.Item2) {
                            
                            AddNewLog("- " + temp.Item1, temp.Item3, Color.blue);
                        
                        } else {

                            AddNewLog("> " + temp.Item1, temp.Item3, Color.blue);
                        }
                    }
                }
            }
            return;
        
        } else {

            nodeList.Add((node.val, false, tabs));
        }

        for (int i = 0; i < node.children.Count; i++) {
            DFS(node.children[i], tabs + 1, nodeList);
        }
    }

    void AddNewLog(string text, int tabs, Color color) {
        
        foreach (GameObject log in logs) {

            log.transform.position -= new Vector3(0, 45f, 0);

        }

        logs.Add(utils.CreateText(containerTransform, -0.12f, 0.35f, tabs, 1400, 45, "", kongtext, 20, color));

        var lastLogText = logs[logs.Count - 1].GetComponent<Text>();
        StartCoroutine(Typing(lastLogText, text));
    }

    IEnumerator Typing(Text lastLogText, string text) {

        for (int i = 0; i < text.Length; i++) {

            lastLogText.text += text[i];
            yield return new WaitForSeconds(0.001f);
        }
    }
}
