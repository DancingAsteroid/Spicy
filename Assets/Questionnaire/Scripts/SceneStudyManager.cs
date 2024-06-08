
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace TeachR.Measurements
{
    public class SceneStudyManager : MonoBehaviour
    {
        public double maxTimeUntilQuestions = 10;

        public bool questionsOpen = false;
        [SerializeField] public List<SceneData> scenes;
        [SerializeField] public int currentScene = 0;
        [SerializeField] public int currentSceneIndex = 0;
        [SerializeField] public int[] sceneOrder = new int[3];
        public double timeUntilQuestions;
        [SerializeField] public GameObject pen;
        [SerializeField] public GameObject tablet;
        [SerializeField] public GameObject handAnimator;
        public double MaxTimeUntilQuestions
        {
            get => maxTimeUntilQuestions;
            set => maxTimeUntilQuestions = value;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            currentScene = sceneOrder[0];
            LoadCurrentScene();
        }

        // Update is called once per frame
        void Update()
        {
            if (questionsOpen)
            {
                return;
            }
            timeUntilQuestions = timeUntilQuestions - Time.deltaTime;
            if (timeUntilQuestions <= 0)
            {
                questionsOpen = true;
                StartQuestionaire();
            }
        }

        public void startNextScene()
        {
            currentSceneIndex += 1;
            if (currentSceneIndex > 2)
            {
                Application.Quit();
                return;
            }
            currentScene = sceneOrder[currentSceneIndex];
            LoadCurrentScene();
        }

        public void LoadCurrentScene()
        {
            if (scenes[currentScene] != null)
            {
                SceneManager.LoadScene(scenes[currentScene].ChemistryRoom);
                timeUntilQuestions = maxTimeUntilQuestions;
                questionsOpen = false;
                GraphicsSettings.renderPipelineAsset = scenes[currentScene].UniversalRenderPipelineAsset;
                QualitySettings.renderPipeline = scenes[currentScene].UniversalRenderPipelineAsset;
                SceneManager.LoadScene(scenes[currentScene].PreviewRooms, LoadSceneMode.Additive);
                pen = GameObject.FindGameObjectWithTag("Pen");
                tablet = GameObject.FindGameObjectWithTag("Tablet");
                handAnimator = GameObject.FindGameObjectWithTag("HandAnimator");
            }

        }

        public void StartQuestionaire()
        {
            pen = GameObject.FindGameObjectWithTag("PenObj");
            tablet = GameObject.FindGameObjectWithTag("Tablet");
            handAnimator = GameObject.FindGameObjectWithTag("HandAnimator");

            foreach (Transform child in pen.transform)
            {
                child.gameObject.SetActive(true);
            }
            foreach (Transform child in tablet.transform)
            {
                child.gameObject.SetActive(true);
            }

            tablet.transform.Find("Questionnaire").GetComponent<Questionnaire>().FragenLaden();
        }
    }
}