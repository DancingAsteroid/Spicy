using UnityEngine;

namespace TeachR.Measurements
{
    public class QuestionnaireComplete : MonoBehaviour
    {

        private Questionnaire questionnaire;
        // Start is called before the first frame update
        void Start()
        {
            questionnaire = GetComponentInParent<Questionnaire>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Stift")
            {
                if (StudyManager.studyManager.end())
                {
                    questionnaire.endScene();
                }
            }
        }
    }
}