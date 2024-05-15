using catalogServiceAPI.Models;

namespace catalogServiceAPI.Services
{
    // Interface til produktrepository, der definerer CRUD-operationer for produkter
    public interface ICatalogRepository
    {
     
        Task AddProduct(Product product);

        
        Task<Product> GetProduct(int ProductID);

      
        Task<List<Product>> GetAllProducts();

     
        Task UpdateProduct(int ProductID, Product product);

    
        Task DeleteProduct(int ProductID);
    }
}