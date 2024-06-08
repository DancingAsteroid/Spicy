using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TeachR.Measurements
{
    public class Questionnaire : MonoBehaviour
    {
        [SerializeField] public int currentPage;
        [SerializeField] public int questionsPerPage;
        [SerializeField] public string condition;

        public int pageCount;
        private GameObject submitButton;
        [SerializeField] private GameObject pagePrefab;

        [SerializeField] private List<Transform> pages = new List<Transform>();
        //private SceneStudyManager sceneStudyManager;


        [SerializeField] public TextAsset frageBogen;

        private int failedSubmitAttempts = 0;
        private int maxFailedSubmitAttempts = 7;
        // Start is called before the first frame update
        void Start()
        {
            submitButton = transform.Find("Submit").gameObject;

            //sceneStudyManager = transform.Find("SceneStudyManager").GetComponent<SceneStudyManager>();
        }

        // Update is called once per frame
        void Update()
        {
            submitButton.SetActive(currentPage == pageCount - 1);
            for (int i = 0; i < pages.Count; i++)
            {
                pages[i].gameObject.SetActive(currentPage == i);
            }
        }

        public void endScene()
        {
            QuestionDataFilled questionDataFilled = new QuestionDataFilled();
            questionDataFilled.answers = new List<IGroupQuestionFilled>();
            // Daten speichern
            foreach (Transform seite in transform.Find("Seiten"))
            {
                questionDataFilled.answers.AddRange(seite.GetComponent<QuestionnaireController>().getQuestionDataFilled());
            }
            bool allQuestionsAnswered = CheckAllQuestionsAnswered(questionDataFilled);

            if (allQuestionsAnswered == false && failedSubmitAttempts < maxFailedSubmitAttempts) {
                failedSubmitAttempts++;
                LevelManager.sharedInstance.NotifyFailedSubmit();
                print("Not all questions in the finished questionaire were answered!");
            } else {
                if (allQuestionsAnswered == false) {
                    Debug.LogWarning("Save file even though not all questions have been answered");
                }
                string saveFile = Application.persistentDataPath + "/" + condition + ".json";
                int i = 0;
                while (File.Exists(saveFile)) {
                    saveFile = Application.persistentDataPath + "/" + condition + i + ".json";
                    i++;
                }
                print("Write Results to: " + saveFile);
                File.WriteAllText(saveFile, JsonUtility.ToJson(questionDataFilled));
                LevelManager.sharedInstance.QuestionnaireFinished();
                //sceneStudyManager.startNextScene(); don't want to use this script in this project
            }
        }

        private bool CheckAllQuestionsAnswered(QuestionDataFilled questionDataFilled) {//Added
            bool allAnswered = true;
            foreach(IGroupQuestionFilled question in questionDataFilled.answers) {
                if (question.value == -1) {
                    allAnswered = false;
                }
            }
            return allAnswered;
        }

        public void FragenLaden()
        {
            QuestionnaireData questionData = JsonUtility.FromJson<QuestionnaireData>(frageBogen.text);
            int anzahlSeiten = questionData.questionnaire.Length / questionsPerPage;
            List<QuestionnaireData> fragenDatas = new List<QuestionnaireData>();
            if (questionData.questionnaire.Length % questionsPerPage > 0)
            {
                anzahlSeiten += 1;
            }

            this.pageCount = anzahlSeiten;
            for (int j = 0; j < anzahlSeiten; j++)
            {
                fragenDatas.Add(new QuestionnaireData());
                fragenDatas[j].questionnaire = new IGroupQuestion[questionData.questionnaire.Length - j * questionsPerPage];
                for (int i = 0; i < questionsPerPage; i++)
                {
                    if ((j * questionsPerPage + i) > questionData.questionnaire.Length - 1)
                    {
                        break;
                    }
                    fragenDatas[j].questionnaire[i] = questionData.questionnaire[j * questionsPerPage + i];
                }
            }

            foreach (var SeiteFragenData in fragenDatas)
            {
                GameObject newSeite = Instantiate(pagePrefab, transform.Find("Seiten"));
                newSeite.GetComponent<QuestionnaireController>().setFragen(SeiteFragenData);
                pages.Add(newSeite.transform);
            }
        }
    }
}