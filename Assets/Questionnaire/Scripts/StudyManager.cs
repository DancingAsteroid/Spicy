using UnityEngine;

public class StudyManager: MonoBehaviour
{
    [SerializeField] public double timeSinceWrite = 0f;
    [SerializeField] public double writeCooldown = 1f;

    [SerializeField] public static StudyManager studyManager;

    void Awake()
    {
        if (StudyManager.studyManager != null && studyManager != this)
        {
            Destroy(this);
        }
        else
        {
            StudyManager.studyManager = this;
        }
    }
    // Start is called before the first frame update
    public bool write()
    {
        bool writingAllowed = timeSinceWrite > writeCooldown;
        if (writingAllowed)
        {
            timeSinceWrite = 0f;
        }
        return writingAllowed;
    }

    public bool end()
    {
        bool writingAllowed = timeSinceWrite > writeCooldown;
        if (writingAllowed)
        {
            writeCooldown = 1f;
        }
        return writingAllowed;
    }

    void Update()
    {
        timeSinceWrite += Time.deltaTime;
    }
}
