using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentEndPoint : MonoBehaviour
{
    public bool isWireEnd1;
    [SerializeField] CircuitComponent component;
    
    public double getComponentEndPointVoltagePotential()
    {
        if (isWireEnd1)
        {
            return component.GetVoltage();
        } else
        {
            return component.GetEndOfComponentVoltage();
        }
    }
}
