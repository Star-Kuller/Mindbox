using CalculationArea.Interfaces;

namespace CalculationArea.Shapes;

// Эта фигура добавлена для демонстрации лёгкости добавления новых фигур.
public class Rectangle : IRectangle
{
    public Rectangle(double horizontalSides, double verticalSides)
    {
        ValidateRectangle(horizontalSides, verticalSides);

        HorizontalSides = horizontalSides;
        VerticalSides = verticalSides;
    }
    
    public double HorizontalSides { get; }
    public double VerticalSides { get; }
    
    private static void ValidateRectangle(double h, double v)
    {
        if (h <= 0 || v <= 0)
            throw new ArgumentException("Sides must be positive");
    }

    public double CalculateArea() => CalculateArea(HorizontalSides, VerticalSides);

    public bool IsSquare() => IsSquare(HorizontalSides, VerticalSides);

    public static double CalculateArea(double horizontalSides, double verticalSides) => horizontalSides * verticalSides;

    public static bool IsSquare(double horizontalSides, double verticalSides)
    {
        return horizontalSides == verticalSides;
        
        // Альтернативная формула, на случай необходимости учёта погрешности
        const double epsilon = 1E-10;
        return Math.Abs(horizontalSides - verticalSides) < epsilon;
    }
}