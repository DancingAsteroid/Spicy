using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal.Internal;

public class Bulb : CircuitComponent, IResistor
{
    // Public members set in Unity Object Inspector
    public GameObject labelResistance;
    public TMP_Text labelResistanceText;
    public GameObject labelCurrent;
    public TMP_Text labelCurrentText;
    public GameObject filament;
    public AudioSource colorChangeAudio;

    float intensity = 0f;

    bool cooldownActive = false;
    Color[] colors = { Color.red, Color.yellow, Color.green, Color.blue, Color.magenta };
    int emissionColorIdx = 1;

    public float Resistance { get; private set; }

    public Bulb() 
    {
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

        if (!isActive)
            DeactivateLight();

        // Set resistance label text
        labelResistanceText.text = Resistance.ToString("0.#") + "Ω";

        // Make sure labels are right side up
        RotateLabel(labelResistance, LabelAlignment.Top);
        RotateLabel(labelCurrent, LabelAlignment.Bottom);
    }

    private void DeactivateLight()
    {
        // Cool down the filament by deactivating the emission
        filament.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");

        //Added: Disable Lighting
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
        // Heat up the filament by activating the emission
        filament.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

        // Set the filament emission color and intensity
        Color baseColor = colors[emissionColorIdx];
        Color finalColor = baseColor * Mathf.Pow(2, intensity);
        filament.GetComponent<Renderer>().material.SetColor("_EmissionColor", finalColor);

        //Added: Activate and Change Color of Point lights if they are found
        Light mainLight = filament.GetComponent<Light>();
        Light closeLight = filament.transform.childCount > 0 ? filament.transform.GetChild(0).GetComponent<Light>() : null ;
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

    public override void SetCurrent(double current)
    {
        Current = current;

        // If we don't have a significant positive current, then we are inactive, even if
        // we are technically part of an active circuit
        if (!IsCurrentSignificant())
        {
            IsActive = false;
            DeactivateLight();
        }
        else
        {
            // Update label text
            labelCurrentText.text = (current * 1000f).ToString("0.#") + "mA";

            // Calculate light intensity based on current
            float maxCurrent = 0.01f;
            float maxIntensity = 5.0f;
            float minIntensity = 3.0f;
            float pctCurrent = ((float)current > maxCurrent ? maxCurrent : (float)current) / maxCurrent;
            intensity = (pctCurrent * (maxIntensity - minIntensity)) + minIntensity;

            ActivateLight();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!cooldownActive && IsActive &&
            (other.gameObject.name.Contains("Pinch")
            || other.gameObject.CompareTag("smallControllerTrigger")))
        {
            // Switch the emission color to the next one in the list
            emissionColorIdx = ++emissionColorIdx % colors.Length;
            ActivateLight();

            StartCoroutine(PlaySound(colorChangeAudio, 0f));

            cooldownActive = true;
            Invoke("Cooldown", 0.5f);
        }
    }

    void Cooldown()
    {
        cooldownActive = false;
    }
}
