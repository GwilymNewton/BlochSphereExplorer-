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


    // Use this for initialization
    void Start()
    {
        //moveToBaseZero();
        //moveToBaseOne();
        //movePosZ();
        //moveNegZ();
        //movePosX();
        moveNegX();
    }

    // Update is called once per frame
    void Update()
    {
        theta = CalcTheta();
        phi = CalcPhi(theta);

        int t = toAngle(theta);
        int p = toAngle(phi);

            updateArrow(t, p);

        Debug.Log("Update Theta: " + theta.ToString() + "("+t+")  Phi" + phi.ToString() + "(" + p + ")");
    
    
    
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
        // Unity Y rotation 0 is pointing right, Bloach is putting towwards you. Hence we adjust.
        // AND, AND WAIT FOR IT, they have a differenat idea of what clockwise is.
        //HENCE
        p = -p + 90;


        //(t,p) -> (t,p)
        // 0  = 0,0
        //1 = 180,0
        //pz(90,90) -> 90,0
        //nz(90,-90) -> 90,-180
        //px(90,0)= 90,90
        //nx(90,180) = 90,-90



        UnityEngine.Vector3 rotation = new Vector3(t, p, 0f);

        Debug.Log(rotation);

        rotator.transform.eulerAngles = rotation;
    }


}
