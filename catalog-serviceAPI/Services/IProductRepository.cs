using catalogServiceAPI.Models;


namespace catalogServiceAPI.Services
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);
        Task<Product> GetProduct(int ProductID);
        Task<List<Product>> GetAllProducts();
        Task UpdateProduct(int ProductID, Product product);
        Task DeleteProduct(int ProductID);
    }
}