using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelResetLever : Lever
{
    public bool ResetToLevelBegin = true;
    public float cooldownTime;
    private bool onCooldown = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        Assert.AreEqual(action, Action.ResetComponents);
        base.Start();
    }

    protected override void ActivateTrigger()
    {
        if (!onCooldown) {
            onCooldown = true;
            StartCoroutine(ResetCooldownAfter(cooldownTime));
            if (ResetToLevelBegin) {
                StartCoroutine(PlaySound(activationSound, 0f));
                LevelManager.sharedInstance.ResetCircuit();
            }
            else {
                base.ActivateTrigger();
            }
        }
    }

    IEnumerator ResetCooldownAfter(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        onCooldown = false;
    }
}
