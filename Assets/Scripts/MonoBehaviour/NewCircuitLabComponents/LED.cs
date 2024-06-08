using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// A LED-light that is conducting and shining if the current flow in the right direction but acts as an isolator in the other direction.#
/// This Implementation is quite lazy, if the LED thinks that it is used with a different current direction then before it changes its resistance and lighting
/// Still is buggy when more then one LED are used...
/// </summary>
public class LED : CircuitComponent, IResistor, IDynamic
{
    public float Resistance { get; private set; }

    // Public members set in Unity Object Inspector
    public GameObject labelResistance;
    public TMP_Text labelResistanceText;
    public GameObject labelCurrent;
    public TMP_Text labelCurrentText;
    public GameObject filament;
    public AudioSource colorChangeAudio;

    public Color colorLED = Color.green;
    float intensity = 0f;

    private bool updateCircuit = false;
    private bool registered = false;
    private bool previousCurrentWasForward = false;
    private float resistanceLow = 0.1f;//Low Resistance if used in right direction. Not 100% accurate to physics but realistic enough.
    private float resistanceHigh = 1000000000000f;//very high resistance makes this basicaly not conducting



    public LED()
    {
        Resistance = resistanceLow;
    }
    ~LED()
    {
        if (registered)
        {
            // Remove ourselves from the circuit lab's list of dynamic objects
            Lab.UnregisterDynamicComponent(this);
        }
    }

    protected override void Update()
    {
        if (!registered)
        {
            // Register as a dynamic component so we'll get coordinated UpdateState calls
            registered = true;
            Lab.RegisterDynamicComponent(this);
        }
        // Show/hide the labels
        bool showLabels = Lab.showLabels && IsActive && IsCurrentSignificant() && !IsShortCircuit;
        labelResistance.gameObject.SetActive(showLabels);
        labelCurrent.gameObject.SetActive(showLabels);
    }

    public override void SetActive(bool isActive, bool isForward)
    {
        IsActive = isActive;
        IsForward = isForward;

        //print("Setactive on LED is called with active and forward " + isActive + isForward);

        if (IsActive && (previousCurrentWasForward != IsForward))
        {
            //print("LED is used in a diffrent direction then in previous setting!");
            //Update conducting and lighting
            if (IsForward)
            {
                Resistance = resistanceHigh;

            }
            else
            {
                Resistance = resistanceLow;
            }
            updateCircuit = true;
        }
        if (IsActive){ // Sometimes a Setactive with IsActive=false is called in between, this shall not change the stored value:
            previousCurrentWasForward = IsForward;
        }

        

        if (!isActive)
            DeactivateLight();

        // Set resistance label text
        labelResistanceText.text = Resistance.ToString("0.#") + "Ω";

        // Make sure labels are right side up
        RotateLabel(labelResistance, LabelAlignment.Top);
        RotateLabel(labelCurrent, LabelAlignment.Bottom);
    }

    public override void SetShortCircuit(bool isShortCircuit, bool isForward)
    {
        IsShortCircuit = isShortCircuit;
        IsForward = isForward;
    }

    public override void SetVoltage(double voltage)
    {
        Voltage = voltage;
    }

    public override void SetCurrent(double current)
    {
        Current = current;

        // If we don't have a significant positive current, then we are inactive, even if
        // we are technically part of an active circuit
        if (!IsCurrentSignificant())
        {
            IsActive = false;
            DeactivateLight();
        } else {
            // Update label text
            labelCurrentText.text = (current * 1000f).ToString("0.#") + "mA";

            // Calculate light intensity based on current
            float maxCurrent = 0.01f;
            float maxIntensity = 3.0f;
            float minIntensity = 1.0f;
            float pctCurrent = ((float)current > maxCurrent ? maxCurrent : (float)current) / maxCurrent;
            intensity = (pctCurrent * (maxIntensity - minIntensity)) + minIntensity;

            ActivateLight();
        }
    }

    public bool UpdateState(int numActiveCircuits)
    {
        if (updateCircuit){
            print("LED lets Lab simulate again!");
        }
        
        // If the state of the LED has been changed since our last UpdateState call, return true
        // to let the circuit lab know that it needs to run an updated simulation.
        bool simulate = updateCircuit;
        updateCircuit = false;
        return simulate;
    }

    private void DeactivateLight()
    {
        filament.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");

        Light mainLight = filament.GetComponent<Light>();
        Light closeLight = filament.transform.childCount > 0 ? filament.transform.GetChild(0).GetComponent<Light>() : null;
        if (mainLight != null) {
            mainLight.enabled = false;
        }
        if (closeLight != null) {
            closeLight.enabled = false;
        }
    }

    private void ActivateLight()
    {
        // Activate the emission
        filament.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

        // Set the emission color and intensity
        Color baseColor = colorLED;
        Color finalColor = baseColor * Mathf.Pow(2, intensity);
        filament.GetComponent<Renderer>().material.SetColor("_EmissionColor", finalColor);

        Light mainLight = filament.GetComponent<Light>();
        Light closeLight = filament.transform.childCount > 0 ? filament.transform.GetChild(0).GetComponent<Light>() : null;
        if (mainLight != null) {
            mainLight.color = baseColor;
            mainLight.enabled = true;
            mainLight.intensity = 0.01f * intensity;
        }
        if (closeLight != null) {
            closeLight.color = baseColor;
            closeLight.enabled = true;
            closeLight.intensity = 0.002f * intensity;
        }
    }

    public override void SelectEntered()
    {
        base.SelectEntered();
    }
}
