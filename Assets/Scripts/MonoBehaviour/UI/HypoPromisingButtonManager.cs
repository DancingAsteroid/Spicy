using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class HypoPromisingButtonManager : HypoButtonManager
{
    [HideInInspector] Color backgroundNormal;
    public Color backgroundFocused;
    [HideInInspector] public HypothesisSelection selectionManager;
    UnityEngine.UI.Button button;

    protected override void Start()
    {
        base.Start();
        backgroundNormal = background.color;
        button = GetComponent<UnityEngine.UI.Button>();
        
    }

    public void EnableButtonInteraction(bool setInteractable)
    {
        button.interactable = setInteractable;
    }

    public void FocusButtonClicked()
    {
        selectionManager.FocusPromisingHypothesis(this);
    }

    public void FocusButton()
    {
        background.color = backgroundFocused;
    }

    public void UnFocusButton()
    {
        background.color = backgroundNormal;
    }

}
