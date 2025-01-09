using SQLRequest;
using SQLRequest.Entities;
using SQLRequest.Interfaces;

//Эти строки следует вынести в AppSettings.json, но в рамках тестового считаю что допустимо оставить тут
const string connectionString = @"Server=(localdb)\mssqllocaldb;Database=ProductsDemo;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False";
const string databaseName = "ProductsDemo";

Console.OutputEncoding = System.Text.Encoding.UTF8;
ILogger logger = new ConsoleLogger();

try
{
    IProductRepository repository = new ProductRepository(connectionString, logger);
    repository.SetupDatabase(databaseName);
    repository.SetupSeedData();

    DisplaySeedData(repository);
    
    var results = repository.GetProductsWithCategories();

    Console.WriteLine("\nРезультат запроса:");
    Console.WriteLine("------------------------");
    foreach (var item in results)
    {
        Console.WriteLine($"Продукт: {item.ProductName,-20} Категория: {item.CategoryName ?? "Без категории"}");
    }
}
catch (Exception ex)
{
    logger.Log($"Произошла ошибка: {ex.Message}");
}

Console.WriteLine("\nНажмите любую клавишу для выхода...");
Console.ReadKey();
return;


void DisplaySeedData(IProductRepository repository)
{
    var allProducts = repository.GetAllProducts();
    Console.WriteLine("\nСписок продуктов:");
    Console.WriteLine("------------------------");
    DisplayRows(allProducts);
    
    var allCategories = repository.GetAllCategories();
    Console.WriteLine("\nСписок категорий:");
    Console.WriteLine("------------------------");
    DisplayRows(allCategories);
}


static void DisplayRows<T>(IEnumerable<T> rows) 
    where T : Entity, IName
{
    foreach (var row in rows)
    {
        Console.WriteLine($"Id: {row.Id, -5} Название: {row.Name}");
    }
}