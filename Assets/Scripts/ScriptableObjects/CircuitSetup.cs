using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ComponentType{
    Wire,
    Lightbulb,
    LED,
    resistor,
    battery,
    staticVoltageSource,
    switchComponent,
    brokenBulb
}

[Serializable]
public class PlaceComonentInstruction{
    public ComponentType componentType;
    public Point startpoint;
    public Point endpoint;

    public Direction DirectionFromInstruction()
    {
        if (startpoint.x == endpoint.x)
        {
            if (startpoint.y - endpoint.y == 1)
            {
                return Direction.South;
            }
            else if (startpoint.y - endpoint.y == -1)
            {
                return Direction.North;
            }
        }
        else if (startpoint.y == endpoint.y)
        {
            if (startpoint.x - endpoint.x == 1)
            {
                return Direction.East;
            }
            else if (startpoint.x - endpoint.x == -1)
            {
                return Direction.West;
            }
        }
        if (componentType != ComponentType.staticVoltageSource){
            Debug.LogError("Startpoint " + startpoint + " and endpoint " + endpoint + "are not adjacent!");
        }
        return Direction.South;//Default value...
    }


}

[CreateAssetMenu(menuName = "CircuitSetup")]
public class CircuitSetup : ScriptableObject
{
    public List<PlaceComonentInstruction> setupInstructions;

}
