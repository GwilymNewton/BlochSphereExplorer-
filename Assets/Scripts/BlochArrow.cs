using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

public class BlochArrow : MonoBehaviour
{

    private Complex alpha = Complex.Zero;
    private Complex beta = Complex.Zero;
    private Complex theta = Complex.Zero;
    private Complex phi = Complex.Zero;

    public GameObject rotator;


    public float speed;

    public Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        moveToBaseZero();
        //moveToBaseOne();
        //movePosZ();
        //moveNegZ();
        //movePosX();
        //moveNegX();
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


    void moveToBaseZero()
    {
        alpha = new Complex(1, 0);
        beta = new Complex(0, 0);
    }

    void moveToBaseOne()
    {
        alpha = new Complex(0, 0);
        beta = new Complex(1, 0);
    }

    void movePosZ()
    {
        alpha = Complex.Divide(Complex.One, Complex.Sqrt(2));
        beta = Complex.Divide(Complex.ImaginaryOne, Complex.Sqrt(2));
    }

    void moveNegZ()
    {
        alpha = Complex.Divide(Complex.One, Complex.Sqrt(2));
        beta = Complex.Divide(Complex.Negate(Complex.ImaginaryOne), Complex.Sqrt(2));
    }

    void movePosX()
    {
        alpha = Complex.Divide(Complex.One, Complex.Sqrt(2));
        beta = Complex.Divide(Complex.One, Complex.Sqrt(2));
    }

    void moveNegX()
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

        // Translation notes Bloch uses Z as vertical, Unity uses Y. Defaulting to unity


        //(t,p) -> (t,p)
        // 0  = 0,0
        //1 = 180,0
        //pz(90,90) -> 90,0
        //nz(90,-90) -> 90,-180
        //px(90,0)= 90,90
        //nx(90,180) = 90,-90



        UnityEngine.Vector3 rotation = new Vector3(t, p, 0f);

        //Debug.Log(rotation);

        rotator.transform.eulerAngles = rotation;
    }

    private void applyMatrix(Complex[,] matrix){

        Complex a = Complex.Add(Complex.Multiply(matrix[0, 0], alpha), Complex.Multiply(matrix[1, 0], beta));
        Complex b = Complex.Add(Complex.Multiply(matrix[0, 1], alpha), Complex.Multiply(matrix[1, 1], beta));

        alpha = a;
        beta = b;

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered: " + other.gameObject.name);
        Complex[,] matrix = other.gameObject.GetComponent<PauliX>().getMatrix();
        applyMatrix(matrix);
    }




}
