using CalculationArea.Interfaces;

namespace CalculationArea.Shapes;

public class Circle : IShape
{
    public Circle(double radius)
    {
        if (radius <= 0)
            throw new ArgumentException("Radius must be positive", nameof(radius));

        Radius = radius;
    }

    public double Radius { get; }

    public double CalculateArea() => CalculateArea(Radius);
    
    public static double CalculateArea(double radius) => Math.PI * radius * radius;
    
}
