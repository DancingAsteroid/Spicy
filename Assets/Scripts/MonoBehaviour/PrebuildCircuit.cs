using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrebuildCircuit : MonoBehaviour
{
    public CircuitSetup circuitSetup;
    public CircuitLab circuitLab; // In this Lab the circuit will be build
    public bool buildAtStart;
    public bool waitAFrame;

    // Start is called before the first frame update
    void Start()
    {
        if (buildAtStart)
        {
            if (waitAFrame) {
                StartCoroutine(BuildCircuitNextFrame());
            }
            else {
                BuildCircuit();
            }
        }
    }

    public void BuildCircuit(){
        circuitLab.Reset();

        CircuitComponent newComponent;
        int instructionnumber = 0;
        Direction componentDirection;
        foreach(PlaceComonentInstruction instruction in circuitSetup.setupInstructions){
            instructionnumber++;
            componentDirection = instruction.DirectionFromInstruction();
            PegMgr startPeg = GetStartPointPeg(instruction.startpoint);
            newComponent = GenerateComponent(instruction.componentType, instructionnumber);
            newComponent.transform.position = startPeg.transform.position;
            startPeg.PrepareComponentforBreadboard(newComponent, instruction.startpoint, instruction.endpoint);
            
            startPeg.UpdateOwnComponentTransformByDirection(newComponent, componentDirection);// Update Position and Rotation
            //startPeg.LockRotation(newComponent.gameObject, newComponent.gameObject);//Update Position and Rotation?
            circuitLab.AddComponent(newComponent.gameObject, instruction.startpoint, instruction.endpoint);

        }
    }

    public void BuildNewCircuit(CircuitSetup newCircuit)
    {
        circuitSetup = newCircuit;

        if (waitAFrame)
        {
            StartCoroutine(BuildCircuitNextFrame());
        }
        else
        {
            BuildCircuit();
        }
    }

    
    private PegMgr GetStartPointPeg(Point start){
        return circuitLab.GetPegMgrFromBoard(start);
    }
    private CircuitComponent GenerateComponent(ComponentType componentType, int numberForName){
        numberForName += 100000; //to avoid naming collisions with Components from dispenser
        GameObject newComponentGO = Instantiate(GameManager.sharedInstance.GetPrefab(componentType));
        newComponentGO.name += numberForName;
        CircuitComponent newComponent = newComponentGO.GetComponent<CircuitComponent>();
        return newComponent;
    }

    private IEnumerator BuildCircuitNextFrame()
    {
        yield return null;
        BuildCircuit();
    }
    

    
}


