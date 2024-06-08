using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides an invisible voltage source that is not suposed to be moved. Inherits from CircuitComponent to make sure that it works corretly with other components of the Circuit Lab.
/// This is supposed to work with prefabs that don't have any visuals. That's also why for this component it is allowed to have startpoints and endpoints that are not adjacent.
/// </summary>
public class LabVoltageSource : CircuitComponent, IBattery
{
    public float BatteryVoltage { get; protected set; }


    public LabVoltageSource()
    {
        BatteryVoltage = 10f;
    }

}
