using SQLRequest.Entities;

namespace SQLRequest.Interfaces;

public interface IProductRepository : IRepository
{
    IEnumerable<Category> GetAllCategories();
    IEnumerable<Product> GetAllProducts();
    IEnumerable<(string ProductName, string CategoryName)> GetProductsWithCategories();
}