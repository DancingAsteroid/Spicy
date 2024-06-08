using System;
using System.Collections;
using System.Collections.Generic;
using TeachR.Measurements;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class Level
{
    public CircuitSetup startCircuit;
    public String problemDefinition;
    public Hypothesis hypothesisSolution;
}

public enum LevelStage
{
    FirstSelection,
    FocusSelection,
    Testing,
    Fixing,
    Evaluation,
    Questionnaire
}

[RequireComponent(typeof(PrebuildCircuit))]
public class LevelManager : MonoBehaviour
{
    public static LevelManager sharedInstance = null;
    [SerializeField] List<Level> levelList;
    [SerializeField] int currentLvl = 0;//The First Level has currentLvL = 1 so this need to be 0 to start at LvL 1 by calling StartNextLvL
    public LevelStage currentStage; //{ get; private set; }
    [SerializeField] InstructionScreen instructionScreen;
    [SerializeField] BasicScreen problemDefinitionScreen;
    [SerializeField] HypothesisSelection hypothesisSelectionScreen;
    [SerializeField] QuestionnaireProvider questionaireTargetLocation;
    

    private PrebuildCircuit circuitBuilder;
    private bool startNextLevelAfterQuestionnaire = false;

    private LevelStage levelStageAfterQuestionnaire;
    private List<LevelStage> questionaireDoneThisLevel = new List<LevelStage>();
    

    void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }

    private void Start()
    {
        circuitBuilder = GetComponent<PrebuildCircuit>();
        Assert.IsNotNull(circuitBuilder.circuitLab);
        Assert.IsNotNull(instructionScreen);
        Assert.IsNotNull(problemDefinitionScreen);
        Assert.IsNotNull(hypothesisSelectionScreen);
        Assert.IsNotNull(questionaireTargetLocation);
        
        StartNextLvL(); 
    }

    public void StartNextLvL()
    {
        currentLvl++;
        if (currentLvl > levelList.Count)
        {
            print("All Level have been finished!");
        } else
        {
            InitiateCurrentLevel();
        }
    }

    public void SetLevelStage(LevelStage newStage){
        if (newStage != LevelStage.Questionnaire && currentStage != LevelStage.Questionnaire && GameManager.sharedInstance.questionnaireScheduler.CheckScheduleAtEndOfStage(currentStage)) {
            levelStageAfterQuestionnaire = newStage;
            StartNewQuestionaire();
            return;
        }
        if (newStage == LevelStage.Questionnaire) {
            hypothesisSelectionScreen.screenBlockingImange.gameObject.SetActive(true);
        } else {
            hypothesisSelectionScreen.screenBlockingImange.gameObject.SetActive(false);
        }

        currentStage = newStage;
        instructionScreen.UpdateScreenText();
    }
    public void SetLevelStageWithoutQuestionnaire(LevelStage newStage) {
        currentStage = newStage;
        instructionScreen.UpdateScreenText();
        hypothesisSelectionScreen.screenBlockingImange.gameObject.SetActive(false);
    }

    private void InitiateCurrentLevel()
    {
        Assert.IsTrue(currentLvl>0 && currentLvl <= levelList.Count);
        Level newLevel = levelList[currentLvl - 1];
        problemDefinitionScreen.SetScreenText(newLevel.problemDefinition);
        questionaireDoneThisLevel.Clear();
        hypothesisSelectionScreen.StartSelectionView();
        
        circuitBuilder.BuildNewCircuit(newLevel.startCircuit);
    }

    public Level GetCurrentLevel()
    {
        return levelList[currentLvl-1];
    }

    public void ResetCircuit()
    {
        circuitBuilder.BuildCircuit();
    }

    public void StartNewQuestionaire() {
        LevelStage levelStageBeforeQuestionaire = currentStage;

        if (questionaireDoneThisLevel.Contains(levelStageBeforeQuestionaire)) {
            return;//Questionaire already done
        }
        questionaireDoneThisLevel.Add(levelStageBeforeQuestionaire);

        if (currentStage == LevelStage.Evaluation) {
            startNextLevelAfterQuestionnaire = true;
        }
        SetLevelStage(LevelStage.Questionnaire);
        
        questionaireTargetLocation.StartNewQuestionaire(levelStageBeforeQuestionaire);
    }

    public void QuestionnaireFinished() {
        questionaireTargetLocation.RemoveCurrentQuestionaireCompletely();
        if (startNextLevelAfterQuestionnaire) {
            startNextLevelAfterQuestionnaire = false;
            StartNextLvL();
        } else {
            if (levelStageAfterQuestionnaire != LevelStage.Questionnaire) {
                SetLevelStage(levelStageAfterQuestionnaire);
            }
        }

    }
    public int GetCurrentLvlNr() {
        return currentLvl;
    }

    public void NotifyFailedSubmit() {
        instructionScreen.AddNotificationNotAllQuestionsAnswered();
    }
}


