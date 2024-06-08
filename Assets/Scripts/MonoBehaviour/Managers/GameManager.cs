using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance = null;
    public QuestionnaireScheduler questionnaireScheduler;
    public int randomGameID { get; private set;}    
    
    [SerializeField] GameObject wirePrefab;
    [SerializeField] GameObject lightbulbPrefab;
    [SerializeField] GameObject LEDPrefab;
    [SerializeField] GameObject resistorPrefab;
    [SerializeField] GameObject batteryPrefab;
    [SerializeField] GameObject staticVoltageSourcePrefab;
    [SerializeField] GameObject switchPrefab;
    [SerializeField] GameObject brokenBulbPrefab;


    void Awake(){
        if (sharedInstance != null && sharedInstance != this){
            Destroy(gameObject);
        } else {
            sharedInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(questionnaireScheduler);
        randomGameID = Random.Range(0, int.MaxValue - 10);
        
    }

    public GameObject GetPrefab(ComponentType componentType){
        GameObject requestedPrefab = null;
        switch (componentType){
            case ComponentType.Wire:
                requestedPrefab = wirePrefab;
                break;
            case ComponentType.Lightbulb:
                requestedPrefab = lightbulbPrefab;
                break;
            case ComponentType.LED:
                requestedPrefab = LEDPrefab;
                break;
            case ComponentType.resistor:
                requestedPrefab = resistorPrefab;
                break;
            case ComponentType.battery:
                requestedPrefab = batteryPrefab;
                break;
            case ComponentType.staticVoltageSource:
                requestedPrefab = staticVoltageSourcePrefab; 
                break;
            case ComponentType.switchComponent:
                requestedPrefab = switchPrefab;
                break;
            case ComponentType.brokenBulb:
                requestedPrefab = brokenBulbPrefab;
                break;
             
        }
        Assert.IsNotNull(requestedPrefab);
        return requestedPrefab;
    }
}


