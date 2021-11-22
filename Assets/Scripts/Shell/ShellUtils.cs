using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellUtils {
    public int uniqLogCode = 0;

    public GameObject CreateText(Transform parentTransform, float x, float y, int tabs, int width, int height, string log, Font font, int fontSize, Color color) {

        GameObject UItextGO = new GameObject("Log" + uniqLogCode);
        UItextGO.tag = "Log";
        UItextGO.transform.SetParent(parentTransform);

        RectTransform rectTransform = UItextGO.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x + tabs * 0.02f, y);
        rectTransform.sizeDelta = new Vector2(width - tabs * 50, height);

        Text text = UItextGO.AddComponent<Text>();
        text.text = log;
        text.alignment = TextAnchor.MiddleLeft;
        text.font = font;
        text.fontStyle = FontStyle.Bold;
        text.fontSize = fontSize;
        text.color = color;

        uniqLogCode++;

        return UItextGO;
    }
}
