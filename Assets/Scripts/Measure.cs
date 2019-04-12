using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Application;
using UnityEngine;

public class Measure : MonoBehaviour, IGate {

    public GateType type = GateType.Measurement;

    // Use this for initialization
    void Start () {
		
	}

    public GateType GetGType()
    {
        return type;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public Complex[,] GetMatrix()
    {
        throw new System.NotImplementedException();
    }
}
