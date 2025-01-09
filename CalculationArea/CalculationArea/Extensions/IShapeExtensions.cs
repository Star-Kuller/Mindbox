using CalculationArea.Interfaces;

namespace CalculationArea.Extensions;

public static class ShapeExtensions
{
    //Метод для расчёта площади фигуры без знания типа фигуры в compile-time
    public static double CalculateArea(this IShape shape)
    {
        return shape.CalculateArea();
    }
}