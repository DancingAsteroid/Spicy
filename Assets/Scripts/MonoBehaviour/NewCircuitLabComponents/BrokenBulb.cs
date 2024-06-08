using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBulb : Bulb
{
    // Start is called before the first frame update
    void Awake() {
        IsClosed = false; //Makes bulb not cunducting
    }


}
