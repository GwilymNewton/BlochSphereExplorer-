using System.Numerics;
using Application;

public interface IGate
{
    Complex[,] GetMatrix();
    GateType GetGType();

}
