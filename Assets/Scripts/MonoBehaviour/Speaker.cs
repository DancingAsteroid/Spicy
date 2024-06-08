using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    [SerializeField] AudioSource questionnaireInfo;
    [SerializeField] AudioSource switchInfo;
    private bool questionnaireInfoNotYetTriggered = true;
    private bool nextTriggerIsSwitchInfo = false;
    public void TryTriggerInfoAudio(){
        if (questionnaireInfo != null && questionnaireInfoNotYetTriggered) {
            questionnaireInfo.Play();
            questionnaireInfoNotYetTriggered=false;
            StartCoroutine(prepareSwitchAudio());
        } 
        if (switchInfo != null && nextTriggerIsSwitchInfo) {
            switchInfo.Play();
            nextTriggerIsSwitchInfo=false;
        }

        IEnumerator prepareSwitchAudio() {
            yield return new WaitForSeconds(5f);
            nextTriggerIsSwitchInfo = true;
        }
        
        
    }
}
