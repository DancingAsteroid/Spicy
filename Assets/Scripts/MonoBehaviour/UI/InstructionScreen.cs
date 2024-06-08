using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionScreen : MonoBehaviour
{
    public string selectionInstruction;
    public string focusSelectionInstruction;
    public string testingInstruction;
    public string fixingInstruction;
    public string evaluationInstruction;
    public string questionnaireInstruction;
    public string notAllQuestionsAnsweredWarning;

    public TMP_Text instructionText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScreenText();
    }

    public void UpdateScreenText()
    {
        switch (LevelManager.sharedInstance.currentStage)
        {
            case LevelStage.FirstSelection:
                instructionText.text = selectionInstruction;
                break;
            case LevelStage.FocusSelection: 
                instructionText.text = focusSelectionInstruction;
                break;
            case LevelStage.Testing: 
                instructionText.text = testingInstruction;
                break;
            case LevelStage.Fixing:
                instructionText.text = fixingInstruction;
                break;
            case LevelStage.Evaluation:
                instructionText.text = evaluationInstruction;
                break;
            case LevelStage.Questionnaire:
                instructionText.text = questionnaireInstruction;
                break;
        }
    }
    public void AddNotificationNotAllQuestionsAnswered() {
        instructionText.text = questionnaireInstruction + "\n" + notAllQuestionsAnsweredWarning;
    }
}
