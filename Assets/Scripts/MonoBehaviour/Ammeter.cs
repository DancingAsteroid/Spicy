using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class Ammeter : MonoBehaviour
{
    
    public TMP_Text displayTMP;
    public Transform moveableClampSide;
    public Transform buttonClamp;
    public BoxCollider clampTrigger;
    public BoxCollider bodyCollider;
    Rigidbody rb;
    public float openRotationClamp = -30.0f;
    public float openRotationButton = -30.0f;
    private float closedRotation = 0f;
    private CircuitComponent currentComponent = null;
    private double oldCurrent;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Assert.IsTrue(clampTrigger.isTrigger);
        Assert.IsFalse(bodyCollider.isTrigger);
        Assert.IsNotNull(rb);
    }
    private void Update(){
        if (currentComponent != null && oldCurrent != currentComponent.Current){
            double newCurrent;
            if (currentComponent.GetIsShortCircuit()) {
                newCurrent = 0; //When a measuring at a short circuit component, assume the fuse has blown
            }
            else {
                newCurrent = currentComponent.Current;
            }

            oldCurrent = newCurrent;
            DisplayCurrent(newCurrent);
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.isTrigger == false){//Only look at Events that are triggered by the Ammeters Trigger collider
            //print("Trigger Event mit " + other);
            CircuitComponent component = other.gameObject.GetComponent<CircuitComponent>();
            if (component != null && other is CapsuleCollider){// Only the CapsuleCollider is active on Components on the breadboard
                FixOnComponent(component);
            }
        }
        
    }


    void FixOnComponent(CircuitComponent component){
        rb.isKinematic = true;
        bodyCollider.enabled = false;

        currentComponent = component;
    }

    void DetachFromComponent(){
        rb.isKinematic = false;
        bodyCollider.enabled = true;
        currentComponent = null;
        oldCurrent = 0; //Make sure the update of the display will be consistent in the future
        DisplayCurrent(0);
    }

    public void GrabEnter(){
        OpenClamp(true);
        clampTrigger.enabled = false;
    }
    public void GrabExit(){
        OpenClamp(false);
        DetachFromComponent();//From old Component
        clampTrigger.enabled = true;
    }

    void OpenClamp(bool open)
    {
        Vector3 oldLocalClampEuler = moveableClampSide.localEulerAngles;
        Vector3 oldLocalButtonEuler = buttonClamp.localEulerAngles;
        if (open){
            moveableClampSide.localEulerAngles = new Vector3(oldLocalClampEuler.x, oldLocalClampEuler.y, openRotationClamp);
            buttonClamp.localEulerAngles = new Vector3(oldLocalClampEuler.x, oldLocalClampEuler.y, openRotationButton);
        } else {
            moveableClampSide.localEulerAngles = new Vector3(oldLocalClampEuler.x, oldLocalClampEuler.y, closedRotation);
            buttonClamp.localEulerAngles = new Vector3(oldLocalClampEuler.x, oldLocalClampEuler.y, closedRotation);
        }
    }

    private void DisplayCurrent(double current){
        displayTMP.text = (Mathf.Abs((float)current) * 1000).ToString("0.#") + "mA";
    }


}
