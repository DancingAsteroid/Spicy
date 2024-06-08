using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[Serializable]
public enum hypothesisJudgement
{
    isCause,
    isNotCause,
    dontKnow,
    notYetSelected
}


public class HypoButtonEvaluation : HypoButtonManager
{
    [HideInInspector] public HypothesisSelection selectionManager;
    [HideInInspector] Color backgroundNormal;
    public Color backgroundFocused;
    public hypothesisJudgement playerJudgement  = hypothesisJudgement.notYetSelected;
    public List<Image> solutionHighlightFrames;

    // Start is called before the first frame update
    protected override void Start()//Don't call UpdateText()!
    {
        Assert.IsTrue(solutionHighlightFrames.Count == 3);
    }
    void ButtonToggeld(hypothesisJudgement jugementOfToggle, Toggle toggleSource) {
        if (toggleSource.isOn) {
            playerJudgement = jugementOfToggle ;
            selectionManager.JudgementSelected(this);
        }
    }
    public void ButtonToggeld1(Toggle toggleSource) {
        ButtonToggeld(hypothesisJudgement.isCause, toggleSource);
    }

    public void ButtonToggeld2(Toggle toggleSource) {
        ButtonToggeld(hypothesisJudgement.isNotCause, toggleSource);
    }
    public void ButtonToggeld3(Toggle toggleSource) {
        ButtonToggeld(hypothesisJudgement.dontKnow, toggleSource);
    }

    public void FocusButton()
    {
        background.color = backgroundFocused;
    }

    public void UnFocusButton()
    {
        background.color = backgroundNormal;
    }

    public void highlightSolution (bool hypoIsSolution) {
        solutionHighlightFrames[0].enabled = true;
        solutionHighlightFrames[1].enabled = true;
        if (hypoIsSolution) {
            solutionHighlightFrames[0].color = Color.green;
            solutionHighlightFrames[1].color = Color.red;
        }
        else {
            solutionHighlightFrames[0].color = Color.red;
            solutionHighlightFrames[1].color = Color.green;
        }

    }



}
