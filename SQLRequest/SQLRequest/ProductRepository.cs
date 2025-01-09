using Dapper;
using Microsoft.Data.SqlClient;
using SQLRequest.Entities;
using SQLRequest.Interfaces;

namespace SQLRequest;

public class ProductRepository(string connectionString, ILogger? logger = null) : IProductRepository
{
    private readonly ILogger _logger = logger ?? new ConsoleLogger(false);
    
    public IEnumerable<Category> GetAllCategories()
    {
        const string sql = """
                           SELECT * FROM Categories c
                           ORDER BY c.Id
                           """;
        using var connection = new SqlConnection(connectionString);
        
        return connection.Query<Category>(sql);
    }
    
    public IEnumerable<Product> GetAllProducts()
    {
        const string sql = """
                           SELECT * FROM Products c
                           ORDER BY c.Id
                           """;
        using var connection = new SqlConnection(connectionString);
        
        return connection.Query<Product>(sql);
    }
    
    public IEnumerable<(string ProductName,string CategoryName)> GetProductsWithCategories()
    {
        const string sql = """
                           SELECT 
                               p.Name AS ProductName, 
                               c.Name AS CategoryName
                           FROM Products p
                           LEFT JOIN ProductCategories pc ON p.Id = pc.ProductId
                           LEFT JOIN Categories c ON pc.CategoryId = c.Id
                           ORDER BY p.Name
                           """;
        using var connection = new SqlConnection(connectionString);
        
        return connection.Query<(string ProductName,string CategoryName)>(sql);
    }
    
    
    public void SetupDatabase(string databaseName)
    {
        using (var connection = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=master;Trusted_Connection=True;"))
        {
            connection.Execute($"""
                                IF EXISTS (SELECT * FROM sys.databases WHERE name = '{databaseName}')
                                BEGIN
                                    ALTER DATABASE {databaseName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                                    DROP DATABASE {databaseName};
                                END
                                CREATE DATABASE {databaseName};
                                """);
        }

        // Создание таблиц и наполнение данными
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Execute("""
                               CREATE TABLE Products (
                                   Id INT PRIMARY KEY IDENTITY(1,1),
                                   Name NVARCHAR(100) NOT NULL
                               );
                               
                               CREATE TABLE Categories (
                                   Id INT PRIMARY KEY IDENTITY(1,1),
                                   Name NVARCHAR(100) NOT NULL
                               );
                               
                               CREATE TABLE ProductCategories (
                                   ProductId INT,
                                   CategoryId INT,
                                   PRIMARY KEY (ProductId, CategoryId),
                                   FOREIGN KEY (ProductId) REFERENCES Products(Id),
                                   FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
                               );
                               """);
        }

        _logger.Log("База данных успешно создана.");
    }

    public void SetupSeedData()
    {
        using var connection = new SqlConnection(connectionString);
        
        connection.Execute("""
                           INSERT INTO Products (Name) VALUES 
                               (N'Ноутбук'), 
                               (N'Смартфон'), 
                               (N'Планшет'),
                               (N'Принтер');

                           INSERT INTO Categories (Name) VALUES 
                               (N'Электроника'),
                               (N'Компьютеры'),
                               (N'Мобильные устройства');

                           INSERT INTO ProductCategories (ProductId, CategoryId) VALUES 
                               (1, 1), -- Ноутбук - Электроника
                               (1, 2), -- Ноутбук - Компьютеры
                               (2, 1), -- Смартфон - Электроника
                               (2, 3), -- Смартфон - Мобильные устройства
                               (3, 1), -- Планшет - Электроника
                               (3, 3); -- Планшет - Мобильные устройства
                               -- Принтер остается без категории
                           """);
        
        _logger.Log("База данных успешно заполнена.");
    }
}