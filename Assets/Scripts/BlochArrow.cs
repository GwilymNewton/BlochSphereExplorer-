using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.UI;
using Application;
using System;

public class BlochArrow : MonoBehaviour
{

    private Complex alpha = Complex.Zero;
    private Complex beta = Complex.Zero;
    private Complex theta = Complex.Zero;
    private Complex phi = Complex.Zero;

    public GameObject rotator;


    public float speed;

    public Rigidbody rb;

    public Text state_text;

    // Use this for initialization
    void Start()
    {
        moveToBaseZero(); // AAKA POS Z
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }


    // Update is called once per frame
    void Update()
    {
        theta = CalcTheta();
        phi = CalcPhi(theta);

        int t = toAngle(theta);
        int p = toAngle(phi);

            updateArrow(t, p);

        //Debug.Log("Update Theta: " + theta.ToString() + "("+t+")  Phi" + phi.ToString() + "(" + p + ")");
        state_text.text = "α = " + alpha.ToString() + ", β =  " + beta.ToString();



    }




    Complex CalcTheta()
    {
        Complex t = 0;

        t = Complex.Multiply(2,Complex.Acos(alpha));

        return t;
    }

    Complex CalcPhi(Complex t)
    {
        Complex p;

        p = Complex.Log( Complex.Divide (beta, (Complex.Sin(Complex.Divide(t, 2)))));

        p = Complex.Divide(p, Complex.ImaginaryOne);

        //Deal with NAN's
        double pr = (p.Real.Equals(System.Double.NaN)) ? (0) : (p.Real);
        double pi = (p.Imaginary.Equals(System.Double.NaN)) ? (0) : (p.Imaginary);

        return new Complex(pr, pi);
    }


    public void moveToBaseZero() //POS Z
    {
        alpha = new Complex(1, 0);
        beta = new Complex(0, 0);
    }

    public void moveToBaseOne() // Neg Z
    {
        alpha = new Complex(0, 0);
        beta = new Complex(1, 0);
    }

    public void movePosY()
    {
        alpha = Complex.Divide(Complex.One, Complex.Sqrt(2));
        beta = Complex.Divide(Complex.ImaginaryOne, Complex.Sqrt(2));
    }

    public void moveNegY()
    {
        alpha = Complex.Divide(Complex.One, Complex.Sqrt(2));
        beta = Complex.Divide(Complex.Negate(Complex.ImaginaryOne), Complex.Sqrt(2));
    }

    public void movePosX()
    {
        alpha = Complex.Divide(Complex.One, Complex.Sqrt(2));
        beta = Complex.Divide(Complex.One, Complex.Sqrt(2));
    }

    public void moveNegX()
    {
        alpha = Complex.Divide(Complex.One, Complex.Sqrt(2));
        beta = Complex.Divide(Complex.Negate(Complex.One), Complex.Sqrt(2));
    }



    int toAngle(Complex a)
    {
        double rad = a.Real;

        double deg = rad * (180 / Mathf.PI);

        int angle = Mathf.RoundToInt((float)deg);

        return angle;
    }


    void updateArrow(int t, int p)
    {

        // Translation notes Bloch uses Z as vertical, Unity uses Y. Defaulting to bloch


        //(t,p) -> (t,p)
        // 0  = 0,0
        //1 = 180,0
        //pz(90,90) -> 90,0
        //nz(90,-90) -> 90,-180
        //px(90,0)= 90,90
        //nx(90,180) = 90,-90


        p += 90;


        UnityEngine.Vector3 rotation = new Vector3(t, p, 0f);

        //Debug.Log(rotation);

        rotator.transform.eulerAngles = rotation;
    }

    private void applyMatrix(Complex[,] matrix){

        Debug.Log("Old: Alpha -> " + alpha + "beta -> " + beta);

        Complex a = Complex.Add(Complex.Multiply(matrix[0, 0], alpha), Complex.Multiply(matrix[1, 0], beta));
        Complex b = Complex.Add(Complex.Multiply(matrix[0, 1], alpha), Complex.Multiply(matrix[1, 1], beta));

        alpha = a;
        beta = b;
        Debug.Log("New: Alpha -> " + alpha + "beta -> " + beta);

    }

    private void Measure(){

        Complex pa = Complex.Pow(alpha, 2);
        Complex pb = Complex.Pow(beta, 2);

        Debug.Log("Cplx: P(A) -> " + pa + "P(B) -> " + pb);

        double rpa = Math.Abs( pa.Real);
        double rpb = Math.Abs(pb.Real);
        Debug.Log("Real: P(A) -> " + pa + "P(B) -> " + pb);

        double rnd = UnityEngine.Random.Range(1f, 0f);

        if(rnd<=rpa){
            moveToBaseZero();
        }
        else
        {
            moveToBaseOne();
        }


    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered: " + other.gameObject.name);
        IGate gate = (IGate)other.gameObject.GetComponent(other.gameObject.name);

        if(gate.GetGType().Equals(GateType.Single))
        {
            Complex[,] matrix = gate.GetMatrix();

            Debug.Log("| " + matrix[0, 0] + " " + matrix[1, 0] + "|" + System.Environment.NewLine + "| " + matrix[0, 1] + " " + matrix[1, 1] + "|");

            applyMatrix(matrix);
        }
        else if(gate.GetGType().Equals(GateType.Measurement))
        {
            Measure();

        }

    }




}
