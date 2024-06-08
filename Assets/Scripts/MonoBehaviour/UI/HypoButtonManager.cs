using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HypoButtonManager : MonoBehaviour
{
    public Hypothesis hypothesis;
    public TMP_Text buttonText;
    public Image background;
    

    protected virtual void Start()
    {
        UpdateText();
        
    }
    public void UpdateText()
    {
        buttonText.text = hypothesis.hypoText;
    }
}
