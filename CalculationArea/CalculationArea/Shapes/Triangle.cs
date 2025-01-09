using CalculationArea.Interfaces;

namespace CalculationArea.Shapes;

public class Triangle : ITriangle
{

    public Triangle(double sideA, double sideB, double sideC)
    {
        ValidateTriangle(sideA, sideB, sideC);

        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
    }

    public double SideA { get; }
    public double SideB { get; }
    public double SideC { get; }

    private static void ValidateTriangle(double a, double b, double c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
            throw new ArgumentException("Sides must be positive");
        if (a + b < c || b + c < a || a + c < b)
            throw new ArgumentException("Invalid triangle sides");
    }

    public double CalculateArea() => CalculateArea(SideA, SideB, SideC);
    

    public bool IsRightTriangle() => IsRightTriangle(SideA, SideB, SideC);
    
    public static double CalculateArea(double sideA, double sideB, double sideC)
    {
        ValidateTriangle(sideA, sideB, sideC);
        var s = (sideA + sideB + sideC) / 2;
        return Math.Sqrt(s * (s - sideA) * (s - sideB) * (s - sideC));
    }

    public static bool IsRightTriangle(double sideA, double sideB, double sideC)
    {
        ValidateTriangle(sideA, sideB, sideC);
        var sides = new[] { sideA, sideB, sideC };
        Array.Sort(sides);
        var legA = sides[0]; // leg переводится как катет
        var legB = sides[1];
        var hypotenuse = sides[2];
        
        return hypotenuse * hypotenuse == (legA * legA) + (legB * legB);
        
        // Альтернативная формула, на случай необходимости учёта погрешности
        const double epsilon = 1E-10;
        return Math.Abs(
            (hypotenuse * hypotenuse) - ((legA * legA) + (legB * legB))
            ) < epsilon;
    }
}