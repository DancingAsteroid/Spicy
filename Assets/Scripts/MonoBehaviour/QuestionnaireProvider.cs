using System.Collections;
using System.Collections.Generic;
using TeachR.Measurements;
using UnityEngine;

public class QuestionnaireProvider : MonoBehaviour
{
    [SerializeField] GameObject penResetButton;
    [SerializeField] GameObject questionairePrefab;
    [SerializeField] GameObject penPrefab;

    private Questionnaire currentActiveQuestionaire;
    private GameObject currentPen;
    private Vector3 currentPenStartingPosition;

    public void StartNewQuestionaire(LevelStage levelStage) {
        RemoveOldQuestionaireAndButton();
        Questionnaire questionnaire = Instantiate(questionairePrefab, transform).GetComponentInChildren<Questionnaire>();
        questionnaire.condition = "Game_" + GameManager.sharedInstance.randomGameID + "_Level_" + LevelManager.sharedInstance.GetCurrentLvlNr() + "_" + levelStage.ToString();
        currentActiveQuestionaire = questionnaire;
        currentPen = Instantiate(penPrefab, transform);
        currentPenStartingPosition = currentPen.transform.position;
        ShowPenResetButton(true);

        questionnaire.FragenLaden();
    }

    public void RemoveCurrentQuestionaireCompletely() {
        RemoveOldQuestionaireAndButton();
        ShowPenResetButton(false);
    }


     void RemoveOldQuestionaireAndButton() {
        if (currentActiveQuestionaire != null) {
            currentActiveQuestionaire.transform.parent.gameObject.SetActive(false);
            Destroy(currentActiveQuestionaire.transform.parent.gameObject);
            currentActiveQuestionaire = null;
        }
        if (currentPen != null) {
            currentPen.SetActive(false);
            currentPen.tag = "Untagged";
            Destroy(currentPen);
            currentPen = null;
        }
    }

    public void ResetPenPosition() {
        if (currentPen != null) {
            currentPen.transform.position = currentPenStartingPosition;
        }
        
    }

    public void ShowPenResetButton(bool show) {
        if (penResetButton != null) { 
            penResetButton.SetActive(show);
        }
    }

    
}
