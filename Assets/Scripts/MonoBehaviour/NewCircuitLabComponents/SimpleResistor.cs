using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleResistor : CircuitComponent, IResistor
{
    public GameObject labelResistance;
    public TMP_Text labelResistanceText;
    public GameObject labelCurrent;
    public TMP_Text labelCurrentText;

    public float Resistance { get; private set; }

    public SimpleResistor(){
        Resistance = 1000f;
    }

    protected override void Update ()
    {
        // Show/hide the labels
        labelResistance.gameObject.SetActive(IsActive && IsCurrentSignificant() && Lab.showLabels);
        labelCurrent.gameObject.SetActive(IsActive && IsCurrentSignificant() && Lab.showLabels);
    }

    public override void SetActive(bool isActive, bool isForward)
    {
        IsActive = isActive;

        // Set resistance label text
        labelResistanceText.text = Resistance.ToString("0.#") + "Ω";

        // Make sure labels are right side up
        RotateLabel(labelResistance, LabelAlignment.Top);
        RotateLabel(labelCurrent, LabelAlignment.Bottom);
    }

    public override void SetCurrent(double current)
    {
        Current = current;

        // If we don't have a significant positive current, then we are inactive, even if
        // we are technically part of an active circuit
        if (!IsCurrentSignificant())
        {
            IsActive = false;

            // Hide the labels
            labelResistance.gameObject.SetActive(false);
            labelCurrent.gameObject.SetActive(false);
        }
        else
        {
            // Update label text
            labelCurrentText.text = (current * 1000f).ToString("0.#") + "mA";
        }
    }

    
}
