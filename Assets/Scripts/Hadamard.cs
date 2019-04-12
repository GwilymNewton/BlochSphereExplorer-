using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Hadamard : MonoBehaviour,IGate {

    // Use this for initialization
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector2[] UVs = new Vector2[mesh.vertices.Length];
        // Front
        UVs[0] = new Vector2(0.0f, 0.0f);
        UVs[1] = new Vector2(0.333f, 0.0f);
        UVs[2] = new Vector2(0.0f, 0.333f);
        UVs[3] = new Vector2(0.333f, 0.333f);
        // Top
        UVs[4] = new Vector2(0.334f, 0.333f);
        UVs[5] = new Vector2(0.666f, 0.333f);
        UVs[8] = new Vector2(0.334f, 0.0f);
        UVs[9] = new Vector2(0.666f, 0.0f);
        // Back
        UVs[6] = new Vector2(1.0f, 0.0f);
        UVs[7] = new Vector2(0.667f, 0.0f);
        UVs[10] = new Vector2(1.0f, 0.333f);
        UVs[11] = new Vector2(0.667f, 0.333f);
        // Bottom
        UVs[12] = new Vector2(0.0f, 0.334f);
        UVs[13] = new Vector2(0.0f, 0.666f);
        UVs[14] = new Vector2(0.333f, 0.666f);
        UVs[15] = new Vector2(0.333f, 0.334f);
        // Left
        UVs[16] = new Vector2(0.334f, 0.334f);
        UVs[17] = new Vector2(0.334f, 0.666f);
        UVs[18] = new Vector2(0.666f, 0.666f);
        UVs[19] = new Vector2(0.666f, 0.334f);
        // Right        
        UVs[20] = new Vector2(0.667f, 0.334f);
        UVs[21] = new Vector2(0.667f, 0.666f);
        UVs[22] = new Vector2(1.0f, 0.666f);
        UVs[23] = new Vector2(1.0f, 0.334f);
        mesh.uv = UVs;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Complex[,] GetMatrix()
    {

        System.Numerics.Complex[,] matrix = new System.Numerics.Complex[2, 2];

        /*
         *X,Y
         *
         * __1__ |1  1|
         * sqrt2 |1 -1|
         *
         */
        matrix[0, 0] = Complex.Multiply(Complex.Divide(1,Complex.Sqrt(2)),Complex.One);
        matrix[1, 0] = Complex.Multiply(Complex.Divide(1, Complex.Sqrt(2)), Complex.One);
        matrix[0, 1] = Complex.Multiply(Complex.Divide(1, Complex.Sqrt(2)), Complex.One);
        matrix[1, 1] = Complex.Multiply(Complex.Divide(1, Complex.Sqrt(2)), Complex.Negate(Complex.One));

        //Debug.Log(matrix);

        return matrix;

    }
}
