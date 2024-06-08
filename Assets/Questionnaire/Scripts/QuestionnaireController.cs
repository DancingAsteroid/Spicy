using System.Collections.Generic;
using UnityEngine;

namespace TeachR.Measurements
{
    public class QuestionnaireController : MonoBehaviour
    {
        [SerializeField] public QuestionnaireData questionnaireData;
        [SerializeField] public List<GameObject> questionnaireGameObjekte = new List<GameObject>();
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void setFragen(QuestionnaireData questionnaireData)
        {
            this.questionnaireData = questionnaireData;
            int lasti = -1;
            for (int i = 0; i < questionnaireGameObjekte.Count; i++)
            {
                if (this.questionnaireData.questionnaire.Length < i + 1)
                {
                    Destroy(questionnaireGameObjekte[i]);
                }
                else
                {
                    questionnaireGameObjekte[i].GetComponent<Question>().questionText.SetText(questionnaireData.questionnaire[i].question);
                    questionnaireGameObjekte[i].GetComponent<Question>().anchorLeftText.SetText(questionnaireData.questionnaire[i].anchorLeft);
                    questionnaireGameObjekte[i].GetComponent<Question>().anchorRightText.SetText(questionnaireData.questionnaire[i].anchorRight);
                    questionnaireGameObjekte[i].GetComponent<Question>().anchorMiddleText.SetText(questionnaireData.questionnaire[i].anchorMiddle);
                }

            }

            if (lasti > -1)
            {
                for (int i = lasti; i < questionnaireGameObjekte.Count; i++)
                {
                    questionnaireGameObjekte.RemoveAt(lasti);
                }
            }
        }

        public List<IGroupQuestionFilled> getQuestionDataFilled()
        {
            int i = 0;
            List<IGroupQuestionFilled> completedQuestionnaires = new List<IGroupQuestionFilled>();
            foreach (GameObject frageObj in questionnaireGameObjekte)
            {
                if (frageObj == null)
                {
                    break;
                }
                completedQuestionnaires.Add(new IGroupQuestionFilled(questionnaireData.questionnaire[i], frageObj.GetComponent<Question>().GetAnswerValue()));
                i++;
            }

            return completedQuestionnaires;
        }
    }
}