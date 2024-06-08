using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;
using UnityEngine.UI;


public class HypothesisSelection : MonoBehaviour {
    List<Hypothesis> promisingHypothesis;
    List<HypoPromisingButtonManager> promisingHypothesisUIElements;
    List<HypoButtonEvaluation> evaluationUIElements;
    public GameObject PromisingHypothesisUIPrefab;
    public GameObject EvaluationUIPrefab;
    public Transform promisingHyposParent;
    public Transform EvaluationButtonsParent;
    public int maxNumberPromisingHypos = 3;
    public GameObject FirstSelectionUI;
    public GameObject FocusSelecttionUI;
    public GameObject EvaluationUI;
    public RectTransform leftAnchor;
    public RectTransform rightAnchor;

    public UnityEngine.UI.Button confirmSelectionButton;
    public UnityEngine.UI.Button startEvaluationButton;
    public UnityEngine.UI.Button evaluationDoneButton;
    public UnityEngine.UI.Button backToFirstSelectionButton;
    public UnityEngine.UI.Button startNextLevel;

    public Image screenBlockingImange;


    private Hypothesis focusedhypothesis;
    private float flickerTime = 0.1f;
    private bool noJudgementYetThisLevel;


    private void Awake() {
        promisingHypothesis = new List<Hypothesis>();
        promisingHypothesisUIElements = new List<HypoPromisingButtonManager>();
        evaluationUIElements = new List<HypoButtonEvaluation>();
    }
    //StartSelectionView(); This will be called by LevelManager


    /// <summary>
    /// Select a hypothesis that the player wants to check whether it is the reason why the circuit isn't working.
    /// </summary>
    public void SelectPromisingHypothesis(Hypothesis hypothesis)
    {
        if (promisingHypothesis.Contains(hypothesis) == false) {
            if (promisingHypothesis.Count >= maxNumberPromisingHypos) {
                RemoveOldestPromisingHypothesis();
            }
            promisingHypothesis.Add(hypothesis);
            promisingHypothesisUIElements.Add(BuildPromisingHypothesisUIElement(hypothesis));
        }
        
    }

    public void ButtonConfirmSelection()
    {
        if (LevelManager.sharedInstance.currentStage == LevelStage.FirstSelection)
        {
            if (promisingHypothesis.Count > 0 ) {
                StartFocusedSelection();
            } else {
                StartCoroutine(FlickerButton(confirmSelectionButton));
            }
        } else if (LevelManager.sharedInstance.currentStage == LevelStage.FocusSelection) {
            if (focusedhypothesis != null) {
                StartTestingView();
            } else {
                StartCoroutine(FlickerButton(confirmSelectionButton));
            }
        }
    }
    public void ButtonEndEvaluation() {
        if (true) {//ToDo: Check if player did complete Evaluation
            GiveFeedback();
            evaluationDoneButton.gameObject.SetActive(false);
            startNextLevel.gameObject.SetActive(true);
            //At End of Evaluation there is no new stage so manually check whether to start a Questionnaire
            if (GameManager.sharedInstance.questionnaireScheduler.CheckScheduleAtEndOfStage(LevelStage.Evaluation)) {
                LevelManager.sharedInstance.StartNewQuestionaire();
            }
            
        }
        else {
            StartCoroutine(FlickerButton(evaluationDoneButton));
        }
    }
    public void StartSelectionView()
    {
        EvaluationUI.SetActive(false);
        FirstSelectionUI.SetActive(true);
     
        ResetToBeginning();
        
        
        FocusSelecttionUI.transform.SetParent(rightAnchor, false);
        EnablePromisingHyposButtonInteraction(false);

        LevelManager.sharedInstance.SetLevelStageWithoutQuestionnaire(LevelStage.FirstSelection);//To be able to go back safely
        
    }

    public void StartFocusedSelection()
    {
        FirstSelectionUI.SetActive(false);
        FocusSelecttionUI.transform.SetParent(leftAnchor, false);
        EnablePromisingHyposButtonInteraction(true);

        LevelManager.sharedInstance.SetLevelStage(LevelStage.FocusSelection);
    }

    void StartTestingView()
    {
        
        FocusSelecttionUI.transform.SetParent(leftAnchor, false) ; 
        EnablePromisingHyposButtonInteraction(true);


        confirmSelectionButton.gameObject.SetActive(false);
        EvaluationUI.SetActive(true);
        evaluationUIElements = BuildEvaluationUIElements();
        ShowEvaluationUIElementsOnlyForFocusedHypothesis();
        
        backToFirstSelectionButton.gameObject.SetActive(true);

        LevelManager.sharedInstance.SetLevelStage(LevelStage.Testing);
    }
    
    void ResetToBeginning() {
        promisingHypothesis.Clear();
        foreach (HypoPromisingButtonManager oldButtonManager in promisingHypothesisUIElements) {
            oldButtonManager.gameObject.transform.SetParent(null);//Let go of Layout Group
            Destroy(oldButtonManager.gameObject);
        }
        promisingHypothesisUIElements.Clear();
        ClearOldEvaluationstuff();
        confirmSelectionButton.gameObject.SetActive(true);
        backToFirstSelectionButton.gameObject.SetActive(false);
        startEvaluationButton.gameObject.SetActive(false);
        evaluationDoneButton.gameObject.SetActive(false);
        startNextLevel.gameObject.SetActive(false);


        focusedhypothesis = null;
        noJudgementYetThisLevel = true;
    }

    

    private void ClearOldEvaluationstuff()
    {
        foreach (HypoButtonEvaluation buttonManager in evaluationUIElements) {
            buttonManager.transform.parent.SetParent(null); //Let go of Layout Group
            Destroy(buttonManager.transform.parent.gameObject);
        }
        evaluationUIElements.Clear();
    }

    public void FirstTestingDone() {
        startEvaluationButton.gameObject.SetActive(true);

        LevelManager.sharedInstance.SetLevelStage(LevelStage.Fixing);
    }

    public void StartEvalueation()
    {
        ShowAllEvaluationUIElemements();
        evaluationDoneButton.gameObject.SetActive(true);
        startEvaluationButton.gameObject.SetActive(false);
        LevelManager.sharedInstance.SetLevelStage(LevelStage.Evaluation);
    }

    

    void GiveFeedback() {
        //Erstmal nur fuer wenn die richtige Hypothese unter den gewaehlten ist
        foreach(HypoButtonEvaluation hypoButtonEvaluation in evaluationUIElements) {
            bool currentIsSolution = (hypoButtonEvaluation.hypothesis == LevelManager.sharedInstance.GetCurrentLevel().hypothesisSolution);
            hypoButtonEvaluation.highlightSolution(currentIsSolution);
        }
    }

    private void EnablePromisingHyposButtonInteraction(bool setInteractable){
        foreach(HypoPromisingButtonManager promisingHypoManager in promisingHypothesisUIElements)
        {
            promisingHypoManager.EnableButtonInteraction(setInteractable);
        }
    }

    public void JudgementSelected(HypoButtonEvaluation originButton) {
        if (noJudgementYetThisLevel) {
            FirstTestingDone();
            noJudgementYetThisLevel = false;
        }
    }

    private List<HypoButtonEvaluation> BuildEvaluationUIElements()
    {
        List<HypoButtonEvaluation> newElements = new List<HypoButtonEvaluation>(); 
        foreach (Hypothesis hypothesis in promisingHypothesis)
        {
            GameObject newUIElement = Instantiate(EvaluationUIPrefab, EvaluationButtonsParent);
            HypoButtonEvaluation newButtonManager = newUIElement.GetComponentInChildren<HypoButtonEvaluation>();
            newButtonManager.hypothesis = hypothesis;
            newButtonManager.selectionManager = this;
            newElements.Add(newButtonManager);
        }
        return newElements;
    }

    private void ShowEvaluationUIElementsOnlyForFocusedHypothesis() {
        foreach (HypoButtonEvaluation evaluationButton in evaluationUIElements) {
            if (evaluationButton.hypothesis == focusedhypothesis) { 
                evaluationButton.gameObject.SetActive(true);
            } else {
                evaluationButton.gameObject.SetActive(false);
            }
        }
    }

    private void ShowAllEvaluationUIElemements() {
        foreach (HypoButtonEvaluation evaluationButton in evaluationUIElements) {
            evaluationButton.gameObject.SetActive(true);
        }
    }

    private void RemoveOldestPromisingHypothesis()
    {
        promisingHypothesis.RemoveAt(0);
        Destroy(promisingHypothesisUIElements[0].gameObject);
        promisingHypothesisUIElements.RemoveAt(0);
    }

    private HypoPromisingButtonManager BuildPromisingHypothesisUIElement(Hypothesis hypothesis)
    {
        GameObject newUIElement = Instantiate(PromisingHypothesisUIPrefab, promisingHyposParent);
        HypoPromisingButtonManager buttonManager = newUIElement.GetComponent<HypoPromisingButtonManager>();
        buttonManager.selectionManager = this;
        buttonManager.hypothesis = hypothesis;
        buttonManager.UpdateText();
        return buttonManager; 
    }

    public void FocusPromisingHypothesis(HypoPromisingButtonManager buttonOfHypo) {
        HighlightPromisingHypothesis(buttonOfHypo);
        focusedhypothesis = buttonOfHypo.hypothesis;
        if (LevelManager.sharedInstance.currentStage == LevelStage.Testing || LevelManager.sharedInstance.currentStage == LevelStage.Fixing) {
            ShowEvaluationUIElementsOnlyForFocusedHypothesis();
        }
    }

    private void HighlightPromisingHypothesis(HypoPromisingButtonManager buttonOfHypo)
    {
        foreach(HypoPromisingButtonManager HypoUIButtonManager in promisingHypothesisUIElements)
        {
            HypoUIButtonManager.UnFocusButton();
        }
        buttonOfHypo.FocusButton();
        
    }
    public void ButtonStartNextLevel() {
        LevelManager.sharedInstance.StartNextLvL();
    }

    IEnumerator FlickerButton (UnityEngine.UI.Button button){
        Color oldColor = button.targetGraphic.color;
        button.targetGraphic.color = Color.red;
        yield return new WaitForSeconds(flickerTime);
        button.targetGraphic.color = oldColor;
    }
}
