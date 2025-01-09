using CalculationArea.Shapes;

namespace CalculationArea.Tests.Shapes;

public class RectangleTests
{
    // Тесты конструктора
    [Theory]
    [InlineData(4, 5)]
    [InlineData(3, 3)]
    [InlineData(2.5, 3.5)]
    public void Constructor_WithValidSides_CreatesRectangle(double h, double v)
    {
        // Arrange & Act
        var rectangle = new Rectangle(h, v); 
        
        // Assert
        Assert.Equal(h, rectangle.HorizontalSides);
        Assert.Equal(v, rectangle.VerticalSides);
    }

    [Theory]
    [InlineData(0, 5, "Sides must be positive")]
    [InlineData(4, 0, "Sides must be positive")]
    [InlineData(-1, 5, "Sides must be positive")]
    [InlineData(4, -1, "Sides must be positive")]
    [InlineData(0, 0, "Sides must be positive")]
    public void Constructor_WithInvalidSides_ThrowsArgumentException(
        double h, double v, string expectedMessage)
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Rectangle(h, v));
        Assert.Equal(expectedMessage, exception.Message);
    }

    // Тесты вычисления площади
    [Theory]
    [InlineData(4, 5, 20)] // Обычный прямоугольник
    [InlineData(3, 3, 9)] // Квадрат
    [InlineData(2.5, 4, 10)] // Дробные значения
    public void CalculateArea_WithValidRectangle_ReturnsCorrectArea(
        double h, double v, double expectedArea)
    {
        // Arrange
        var rectangle = new Rectangle(h, v);

        // Act
        var area = rectangle.CalculateArea();

        // Assert
        Assert.Equal(expectedArea, area);
    }

    [Theory]
    [InlineData(4, 5, 20)]
    [InlineData(3, 3, 9)]
    [InlineData(2.5, 4, 10)]
    public void StaticCalculateArea_WithValidRectangle_ReturnsCorrectArea(
        double h, double v, double expectedArea)
    {
        // Act
        var area = Rectangle.CalculateArea(h, v);

        // Assert
        Assert.Equal(expectedArea, area);
    }

    // Тесты на проверку квадрата
    [Theory]
    [InlineData(3, 3)]
    [InlineData(5, 5)]
    [InlineData(2.5, 2.5)]
    public void IsSquare_WithEqualSides_ReturnsTrue(double h, double v)
    {
        // Arrange
        var rectangle = new Rectangle(h, v);

        // Act
        var isSquare = rectangle.IsSquare();

        // Assert
        Assert.True(isSquare);
    }

    [Theory]
    [InlineData(2, 3)]
    [InlineData(4, 5)]
    [InlineData(2.5, 3.5)]
    public void IsSquare_WithUnequalSides_ReturnsFalse(double h, double v)
    {
        // Arrange
        var rectangle = new Rectangle(h, v);

        // Act
        var isSquare = rectangle.IsSquare();

        // Assert
        Assert.False(isSquare);
    }

    // Тесты сравнения статического и инстанс методов
    [Theory]
    [InlineData(4, 5)]
    [InlineData(3, 3)]
    public void CalculateAreaInstanceAndStaticMethods_ReturnSameResults(double h, double v)
    {
        // Arrange
        var rectangle = new Rectangle(h, v);

        // Act
        var instanceArea = rectangle.CalculateArea();
        var staticArea = Rectangle.CalculateArea(h, v);

        // Assert
        Assert.Equal(instanceArea, staticArea);
    }
    
    [Theory]
    [InlineData(4, 5)]
    [InlineData(3, 3)]
    public void IsSquareInstanceAndStaticMethods_ReturnSameResults(double h, double v)
    {
        // Arrange
        var rectangle = new Rectangle(h, v);

        // Act
        var instanceIsSquare = rectangle.IsSquare();
        var staticIsSquare = Rectangle.IsSquare(h, v);

        // Assert
        Assert.Equal(instanceIsSquare, staticIsSquare);
    }

    // Дополнительные тесты на свойства прямоугольника
    [Theory]
    [InlineData(4, 5)]
    [InlineData(3, 3)]
    public void Rectangle_AreaIsLessThanSquareOfPerimeter(double h, double v)
    {
        // Arrange
        var rectangle = new Rectangle(h, v);
        var perimeter = 2 * (h + v);

        // Act
        var area = rectangle.CalculateArea();

        // Assert
        Assert.True(area < perimeter * perimeter);
    }

    // Тесты на граничные случаи
    [Theory]
    [InlineData(double.MaxValue / 2, 2)] // Большие числа
    public void Rectangle_WithLargeNumbers_CalculatesCorrectly(double h, double v)
    {
        // Arrange
        var rectangle = new Rectangle(h, v);

        // Act
        var area = rectangle.CalculateArea();

        // Assert
        Assert.Equal(h * v, area);
    }

    [Theory]
    [InlineData(1e-10, 1e-10)] // Очень маленькие числа
    public void Rectangle_WithVerySmallNumbers_CalculatesCorrectly(double h, double v)
    {
        // Arrange
        var rectangle = new Rectangle(h, v);

        // Act
        var area = rectangle.CalculateArea();

        // Assert
        Assert.Equal(h * v, area);
    }

    // Тест на коммутативность сторон
    [Fact]
    public void Rectangle_AreaIsCommutative()
    {
        // Arrange
        var rectangle1 = new Rectangle(2, 3);
        var rectangle2 = new Rectangle(3, 2);

        // Act
        var area1 = rectangle1.CalculateArea();
        var area2 = rectangle2.CalculateArea();

        // Assert
        Assert.Equal(area1, area2);
    }
}