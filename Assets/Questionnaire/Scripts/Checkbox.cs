using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkbox : MonoBehaviour
{
    [SerializeField] public  bool isChecked;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Transform penTip;
    [SerializeField] private GameObject check;
    [SerializeField] private Question frage;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        penTip = GameObject.FindGameObjectWithTag("Stift").transform;
        check = transform.Find("Check").gameObject;
        frage = transform.parent.parent.GetComponent<Question>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boxCollider.bounds.Contains(penTip.position))
        {
            if (StudyManager.studyManager.write())
            {
                if (!isChecked == true)
                {
                    frage.Check(this);
                }
            }
        }
        check.SetActive(isChecked);
    }

    public void setCheck(bool check)
    {
        isChecked = check;
        this.check.SetActive(isChecked);
    }
}
