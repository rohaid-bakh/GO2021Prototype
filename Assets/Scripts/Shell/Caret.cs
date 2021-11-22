using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caret : MonoBehaviour
{
    public InputField inputField;
    public Text textComp;
    public Canvas canvas;

    public GameObject caret;

    private string oldValue = "";
    private string newValue = "";

    public float offset;

    public Vector3 whiteSpaceSize;

    public bool isFocus = true;

    // Start is called before the first frame update
    void Start() {

        inputField.ActivateInputField();
    }

    // Update is called once per frame
    void Update() {

        oldValue = newValue;
        newValue = textComp.text;

        if (!oldValue.Equals(newValue)) {
            int length = textComp.text.Length;
            if (length <= 0) {
                
                caret.transform.position = new Vector3(790, 1015, 0);
            
            } else {

                caret.transform.position = new Vector3(GetWorldPos(length - 1).x, 1015, 0);
            }

        }

        if (!isFocus && inputField.isFocused) {

            caret.GetComponent<SpriteRenderer>().color = Color.white;
            isFocus = true;

        } else if (isFocus && !inputField.isFocused) {
            
            caret.GetComponent<SpriteRenderer>().color = Color.red;
            isFocus = false;
        }

        inputField.caretPosition = textComp.text.Length;

    }

    Vector3 GetWorldPos(int charIndex) {

        string text = textComp.text;

        if (charIndex >= text.Length)
            return new Vector3(790, 1015, 0);
        
        TextGenerator textGen = new TextGenerator(text.Length);
        Vector2 extents = textComp.gameObject.GetComponent<RectTransform>().rect.size;
        textGen.Populate(text, textComp.GetGenerationSettings(extents));

        int whiteSpace = 0;

        for (int i = 0; i <= charIndex; i++) {
            
            if (text[i].Equals(' ')) {
                
                whiteSpace++;
            }
        }
        
        int indexOfTextQuad = (charIndex - whiteSpace) * 4;
        
        if (charIndex >= whiteSpace) {
            
            if (indexOfTextQuad < textGen.vertexCount) {

                Vector3 avgPos = new Vector3(offset, 0, 0) + (textGen.verts[indexOfTextQuad].position + textGen.verts[indexOfTextQuad + 1].position + textGen.verts[indexOfTextQuad + 2].position + textGen.verts[indexOfTextQuad + 3].position) / 4f;
                
                if (charIndex > 0 && !text[charIndex].Equals(' ')) {
                
                    return textComp.transform.TransformPoint(avgPos);
                
                } else { 

                    int whiteSpaceFromLastLetter = charIndex - GetAfterLastLetterIdx(text, charIndex) + 1;
                    return textComp.transform.TransformPoint(avgPos + whiteSpaceSize * whiteSpaceFromLastLetter);
                }
            } else {
                
                return new Vector3(790, 1015, 0);
            }
        } else {

            return new Vector3(790, 1015, 0) + whiteSpaceSize * whiteSpace;
        }
    }

    int GetAfterLastLetterIdx(string text, int len_minus_1) {

        for (int i = len_minus_1; i >= 0; i--) {

            if (!text[i].Equals(' ')) {
                
                return i + 1;
            }
        }
        return 0;
    }
}
