using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : MonoBehaviour
{
    public GameObject[] textContainers = new GameObject[6];
    public string[] texts = {"CNCT Windows [Version 11.0.20031.1344]", "C:\\Users\\Jo_Hopper>", "ls", "> Projects", "> Techno_Bugs", "- Bugs_Data.txt \n Click Me! :)"};
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FillContainersWithTexts());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FillContainersWithTexts() {
        // Gives enough time for the loading transition to finish;
        
        yield return new WaitForSeconds(1f);
        int i = 0;
        string text = texts[i];
        int t = 0;
        
        while (i < textContainers.Length) {
            
            textContainers[i].GetComponent<Text>().text += text[t];

            t += 1;
            
            yield return new WaitForSeconds(0.005f);

            if (t == text.Length) {
                
                i += 1;
                t = 0;
                
                if (i < textContainers.Length) {
                    
                    text = texts[i];
                }
                
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
