using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    

    public TMP_Text fpsText;
    public float deltaTimeForFPS;

    void Update() {
        deltaTimeForFPS += (Time.deltaTime - deltaTimeForFPS) * 0.1f;
        float fps = 1.0f / Time.unscaledDeltaTime;
        fpsText.text = Mathf.RoundToInt(fps).ToString();
    }
}
