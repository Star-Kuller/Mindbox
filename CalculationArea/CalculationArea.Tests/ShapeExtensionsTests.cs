using CalculationArea.Extensions;
using CalculationArea.Interfaces;
using CalculationArea.Shapes;

namespace CalculationArea.Tests;

public class ShapeExtensionsTests
{
    [Fact]
    public void CalculateArea_WithCircle_ReturnsCorrectArea()
    {
        // Arrange
        IShape shape = new Circle(5);
        const double expectedArea = Math.PI * 25;

        // Act
        var area = shape.CalculateArea();

        // Assert
        Assert.Equal(expectedArea, area, precision: 10);
    }

    [Fact]
    public void CalculateArea_WithTriangle_ReturnsCorrectArea()
    {
        // Arrange
        IShape shape = new Triangle(3, 4, 5);
        const int expectedArea = 6;

        // Act
        var area = shape.CalculateArea();

        // Assert
        Assert.Equal(expectedArea, area, precision: 10);
    }

    [Fact]
    public void CalculateArea_WithRectangle_ReturnsCorrectArea()
    {
        // Arrange
        IShape shape = new Rectangle(4, 5);
        const int expectedArea = 20;

        // Act
        var area = shape.CalculateArea();

        // Assert
        Assert.Equal(expectedArea, area, precision: 10);
    }

    [Fact]
    public void CalculateArea_WithDifferentShapes_ReturnsConsistentResults()
    {
        // Arrange
        IShape circle = new Circle(5);
        IShape triangle = new Triangle(3, 4, 5);
        IShape rectangle = new Rectangle(4, 5);

        // Act
        var circleArea = circle.CalculateArea();
        var triangleArea = triangle.CalculateArea();
        var rectangleArea = rectangle.CalculateArea();

        // Assert
        Assert.Equal(Math.PI * 25, circleArea, precision: 10);
        Assert.Equal(6, triangleArea, precision: 10);
        Assert.Equal(20, rectangleArea);
    }

    [Fact]
    public void CalculateArea_WithSameShapeDifferentInstances_ReturnsEqualAreas()
    {
        // Arrange
        IShape circle1 = new Circle(5);
        IShape circle2 = new Circle(5);

        // Act
        var area1 = circle1.CalculateArea();
        var area2 = circle2.CalculateArea();

        // Assert
        Assert.Equal(area1, area2);
    }

    [Fact]
    public void CalculateArea_ExtensionMethodVsDirectCall_ReturnsSameResult()
    {
        // Arrange
        var circle = new Circle(5);

        // Act
        var extensionResult = ShapeExtensions.CalculateArea(circle);
        var directResult = circle.CalculateArea();

        // Assert
        Assert.Equal(extensionResult, directResult);
    }

    [Theory]
    [InlineData(1)] // Маленький круг
    [InlineData(1000)] // Большой круг
    public void CalculateArea_WithDifferentSizeCircles_ReturnsCorrectArea(double radius)
    {
        // Arrange
        IShape shape = new Circle(radius);
        var expectedArea = Math.PI * radius * radius;

        // Act
        var area = shape.CalculateArea();

        // Assert
        Assert.Equal(expectedArea, area, precision: 10);
    }

    [Theory]
    [InlineData(3, 4, 5)] // Прямоугольный треугольник
    [InlineData(5, 5, 5)] // Равносторонний треугольник
    public void CalculateArea_WithDifferentTriangles_ReturnsCorrectArea(
        double a, double b, double c)
    {
        // Arrange
        IShape shape = new Triangle(a, b, c);
        var s = (a + b + c) / 2;
        var expectedArea = Math.Sqrt(s * (s - a) * (s - b) * (s - c));

        // Act
        var area = shape.CalculateArea();

        // Assert
        Assert.Equal(expectedArea, area, precision: 10);
    }

    [Theory]
    [InlineData(4, 5)] // Прямоугольник
    [InlineData(5, 5)] // Квадрат
    public void CalculateArea_WithDifferentRectangles_ReturnsCorrectArea(
        double width, double height)
    {
        // Arrange
        IShape shape = new Rectangle(width, height);
        var expectedArea = width * height;

        // Act
        var area = shape.CalculateArea();

        // Assert
        Assert.Equal(expectedArea, area);
    }
    

    // Тест на null
    [Fact]
    public void CalculateArea_WithNull_ThrowsArgumentNullException()
    {
        // Arrange
        IShape shape = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => shape.CalculateArea());
    }
}