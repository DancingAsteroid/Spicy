using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hypothesis")]
public class Hypothesis : ScriptableObject
{
    public HypoID ID;
    public string hypoText;
}

public enum HypoID
{
    notClosed,
    shotCircuit,
    wrongDirection,
    currentNotEnough,
    moreVoltageNeeded,
    LEDnoResistance,
    wrongCircuit,
    defectBulb,
    defectCable

}
