using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public string state = "Liquid";
    public float evaporationPoint = 100f;
    public float solidifyingPoint = 0f;
    public float combustiblePoint = 500f;

    public void React(GameObject other){
        
    }

}
