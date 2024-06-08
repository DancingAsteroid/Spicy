using UnityEngine;

namespace TeachR.Measurements
{
    public class QuestionnaireArrow : MonoBehaviour
    {
        public int amountAddedToSiteCount = 1;
        [SerializeField] QuestionnaireArrow otherArrowButton;
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
                if (StudyManager.studyManager.write())
                {
                    questionnaire.currentPage += amountAddedToSiteCount;
                    
                    if (amountAddedToSiteCount > 0 && questionnaire.currentPage >= questionnaire.pageCount - 1) gameObject.SetActive(false);
                    if (amountAddedToSiteCount < 0 && questionnaire.currentPage <= 0) gameObject.SetActive(false);
                    //If a button was pressed, the other one needs to be active so that it is possible to go back!
                    if (otherArrowButton != null && !otherArrowButton.gameObject.activeSelf) otherArrowButton.gameObject.SetActive(true);
                }
            }
        }
    }
}