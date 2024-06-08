using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Question : MonoBehaviour
{
    [SerializeField]public TextMeshPro questionText;
    [SerializeField]public TextMeshPro anchorLeftText;
    [SerializeField]public TextMeshPro anchorRightText;
    [SerializeField]public TextMeshPro anchorMiddleText;
    private List<Checkbox> checkboxes = new List<Checkbox>();
    // Start is called before the first frame update
    void Start()
    {
        checkboxes = transform.Find("Checkboxes").transform.GetComponentsInChildren<Checkbox>().ToList();
    }

    public void Check(Checkbox checkbox)
    {
        for(int i=0; i<checkboxes.Count; i++)
        {
            Checkbox check = checkboxes[i];
            check.setCheck(false);
            if (check == checkbox)
            {
                check.setCheck(true);
            }
        }
    }

    public int GetAnswerValue()
    {
        for(int i=0; i<checkboxes.Count; i++)
        {
            Checkbox check = checkboxes[i];
            if (check.isChecked)
            {
                return i;
            }
        }

        return -1;
    }
}
