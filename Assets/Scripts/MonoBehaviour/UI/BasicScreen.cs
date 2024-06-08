using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicScreen : MonoBehaviour
{
    public TMP_Text screenText;

    public void SetScreenText(string newText){
        screenText.text = newText;
    }
}
