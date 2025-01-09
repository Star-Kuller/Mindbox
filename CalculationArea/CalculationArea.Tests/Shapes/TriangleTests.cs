using CalculationArea.Shapes;

namespace CalculationArea.Tests.Shapes;

public class TriangleTests
{
    // Тесты конструктора
    [Theory]
    [InlineData(3, 4, 5)]
    [InlineData(5, 5, 5)]
    [InlineData(2, 3, 4)]
    public void Constructor_WithValidSides_CreatesTriangle(double a, double b, double c)
    {
        // Arrange & Act
        var triangle = new Triangle(a, b, c);

        // Assert
        Assert.Equal(a, triangle.SideA);
        Assert.Equal(b, triangle.SideB);
        Assert.Equal(c, triangle.SideC);
    }

    [Theory]
    [InlineData(0, 4, 5, "Sides must be positive")]
    [InlineData(3, 0, 5, "Sides must be positive")]
    [InlineData(3, 4, 0, "Sides must be positive")]
    [InlineData(-1, 4, 5, "Sides must be positive")]
    [InlineData(1, 1, 3, "Invalid triangle sides")]
    [InlineData(1, 3, 1, "Invalid triangle sides")]
    public void Constructor_WithInvalidSides_ThrowsArgumentException(
        double a, double b, double c, string expectedMessage)
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Triangle(a, b, c));
        Assert.Equal(expectedMessage, exception.Message);
    }

    // Тесты вычисления площади
    [Theory]
    [InlineData(3, 4, 5, 6)] // Прямоугольный треугольник
    [InlineData(5, 5, 5, 10.825317547305483)] // Равносторонний треугольник
    [InlineData(2, 3, 4, 2.9047375096555625)] // Произвольный треугольник
    public void CalculateArea_WithValidTriangle_ReturnsCorrectArea(
        double a, double b, double c, double expectedArea)
    {
        // Arrange
        var triangle = new Triangle(a, b, c);

        // Act
        var area = triangle.CalculateArea();

        // Assert
        Assert.Equal(expectedArea, area, precision: 10);
    }

    [Theory]
    [InlineData(3, 4, 5, 6)] // Те же тестовые данные для статического метода
    [InlineData(5, 5, 5, 10.825317547305483)]
    [InlineData(2, 3, 4, 2.9047375096555625)]
    public void StaticCalculateArea_WithValidTriangle_ReturnsCorrectArea(
        double a, double b, double c, double expectedArea)
    {
        // Act
        var area = Triangle.CalculateArea(a, b, c);

        // Assert
        Assert.Equal(expectedArea, area, precision: 10);
    }

    // Тесты на проверку прямоугольного треугольника
    [Theory]
    [InlineData(3, 4, 5)]
    [InlineData(5, 12, 13)]
    [InlineData(8, 15, 17)]
    public void IsRightTriangle_WithRightTriangle_ReturnsTrue(double a, double b, double c)
    {
        // Arrange
        var triangle = new Triangle(a, b, c);

        // Act
        var isRight = triangle.IsRightTriangle();

        // Assert
        Assert.True(isRight);
    }

    [Theory]
    [InlineData(2, 3, 4)]
    [InlineData(5, 5, 5)]
    public void IsRightTriangle_WithNonRightTriangle_ReturnsFalse(double a, double b, double c)
    {
        // Arrange
        var triangle = new Triangle(a, b, c);

        // Act
        var isRight = triangle.IsRightTriangle();

        // Assert
        Assert.False(isRight);
    }

    // Тесты сравнения статического и инстанс методов
    [Theory]
    [InlineData(3, 4, 5)]
    [InlineData(5, 5, 5)]
    public void CalculateAreaInstanceAndStaticMethods_ReturnSameResults(double a, double b, double c)
    {
        // Arrange
        var triangle = new Triangle(a, b, c);

        // Act
        var instanceArea = triangle.CalculateArea();
        var staticArea = Triangle.CalculateArea(a, b, c);

        // Assert
        Assert.Equal(instanceArea, staticArea);
    }
    
    [Theory]
    [InlineData(3, 4, 5)]
    [InlineData(5, 5, 5)]
    public void IsRightTriangleInstanceAndStaticMethods_ReturnSameResults(double a, double b, double c)
    {
        // Arrange
        var triangle = new Triangle(a, b, c);

        // Act
        var instanceIsRight = triangle.IsRightTriangle();
        var staticIsRight = Triangle.IsRightTriangle(a, b, c);

        // Assert
        Assert.Equal(instanceIsRight, staticIsRight);
    }
}