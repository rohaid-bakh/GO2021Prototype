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

    public bool isFocus = true;

    // Start is called before the first frame update
    void Start() {

        inputField.onValueChanged.AddListener(delegate { Formatting(); });
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

                caret.transform.position = new Vector3(GetWorldPos(length - 1).x + offset, 1015, 0);
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

        int newLine = text.Substring(0, charIndex).Split('\n').Length - 1;
        int whiteSpace = text.Substring(0, charIndex).Split(' ').Length - 1;
        int indexOfTextQuad = (charIndex * 4) + (newLine * 4) - (whiteSpace * 4);

        if (indexOfTextQuad < textGen.vertexCount) {

            Vector3 avgPos = (textGen.verts[indexOfTextQuad].position + textGen.verts[indexOfTextQuad + 1].position + textGen.verts[indexOfTextQuad + 2].position + textGen.verts[indexOfTextQuad + 3].position) / 4f;
            return textComp.transform.TransformPoint(avgPos);

        } else {

            Debug.LogError("Out of text bound");
            return new Vector3(790, 1015, 0);
        }
    }

    void Formatting() {

        inputField.text = inputField.text.Replace(" ", "");
    }
}
