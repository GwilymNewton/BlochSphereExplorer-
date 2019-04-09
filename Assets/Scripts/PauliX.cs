using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PauliX : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Complex[,] getMatrix(){

        System.Numerics.Complex[,] matrix = new System.Numerics.Complex[2, 2];


        /*
         *X,Y
         *
         *|0 1|
         *|1 0|
         *
         */
        matrix[0, 0] = Complex.Zero;
        matrix[1, 0] = Complex.One;
        matrix[0, 1] = Complex.One;
        matrix[1, 1] = Complex.Zero;

        //Debug.Log(matrix);

        return matrix;

    }

}
