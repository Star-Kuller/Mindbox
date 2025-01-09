using CalculationArea.Shapes;

namespace CalculationArea.Tests;

public class CircleTests
{
    [Fact]
    public void Constructor_WithPositiveRadius_CreatesCircle()
    {
        // Arrange & Act
        var circle = new Circle(5);

        // Assert
        Assert.Equal(5, circle.Radius);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Constructor_WithZeroOrNegativeRadius_ThrowsArgumentException(double radius)
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Circle(radius));
        Assert.Equal("Radius must be positive (Parameter 'radius')", exception.Message);
    }

    [Theory]
    [InlineData(1, Math.PI)]
    [InlineData(2, Math.PI * 4)]
    [InlineData(5, Math.PI * 25)]
    public void CalculateArea_WithValidRadius_ReturnsCorrectArea(double radius, double expectedArea)
    {
        // Arrange
        var circle = new Circle(radius);

        // Act
        var area = circle.CalculateArea();

        // Assert
        Assert.Equal(expectedArea, area, precision: 10);
    }

    [Theory]
    [InlineData(1, Math.PI)]
    [InlineData(2, Math.PI * 4)]
    [InlineData(5, Math.PI * 25)]
    public void StaticCalculateArea_WithValidRadius_ReturnsCorrectArea(double radius, double expectedArea)
    {
        // Act
        var area = Circle.CalculateArea(radius);

        // Assert
        Assert.Equal(expectedArea, area, precision: 10);
    }

    [Fact]
    public void InstanceAndStaticMethods_ReturnSameResult()
    {
        // Arrange
        const int radius = 10;
        var circle = new Circle(radius);

        // Act
        var instanceResult = circle.CalculateArea();
        var staticResult = Circle.CalculateArea(radius);

        // Assert
        Assert.Equal(instanceResult, staticResult);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void Circle_AreaIsLargerThanSquareOfRadius(double radius)
    {
        // Arrange
        var circle = new Circle(radius);

        // Act
        var area = circle.CalculateArea();

        // Assert
        Assert.True(area > radius * radius);
    }
}