using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class AfterStageQuestionnaireConditions
{
    public int percentageForQuestionnaire;
    public LevelStage mutuallyExclusiveWithStageInSameLevel = LevelStage.Questionnaire;//Chosing Questionnaire means not mutually exclusive from anything
    
}


public class QuestionnaireScheduler : MonoBehaviour
{
    public AfterStageQuestionnaireConditions[] conditionsForStagesDuringLevelX;
    public AfterStageQuestionnaireConditions[] conditionsForStagesAfterLevelX;
    public int levelX = 2;



    private void Start() {
        Assert.IsTrue(conditionsForStagesDuringLevelX.Length + conditionsForStagesAfterLevelX.Length == 10); // 5 normal stages: FirstSelection, FocusSelection, Testing, Fixing and Evaluation
    }

    public bool CheckScheduleAtEndOfStage(LevelStage stage) {
        AfterStageQuestionnaireConditions[] conditionsForStages;
        if (LevelManager.sharedInstance.GetCurrentLvlNr() < levelX) {
            return false;
        } else if (LevelManager.sharedInstance.GetCurrentLvlNr() == levelX) {
            conditionsForStages = conditionsForStagesDuringLevelX;
        } else {
            conditionsForStages = conditionsForStagesAfterLevelX;
        }
        return CheckScheduleForConditions(stage, conditionsForStages); ;
    }

    private bool CheckScheduleForConditions(LevelStage stage, AfterStageQuestionnaireConditions[] conditionsForStages) {
        AfterStageQuestionnaireConditions stageCondition = conditionsForStages[(int)stage];
        bool pleaseDoAQuestionaire = UnityEngine.Random.Range(0, 100) < stageCondition.percentageForQuestionnaire;
        if (stageCondition.mutuallyExclusiveWithStageInSameLevel != LevelStage.Questionnaire) {//Questionnaire is the default value
            if (pleaseDoAQuestionaire) {
                conditionsForStages[(int)stageCondition.mutuallyExclusiveWithStageInSameLevel].percentageForQuestionnaire = 0;
            }
            else {
                conditionsForStages[(int)stageCondition.mutuallyExclusiveWithStageInSameLevel].percentageForQuestionnaire = 100;
            }
        }

        return pleaseDoAQuestionaire;
    }
}
