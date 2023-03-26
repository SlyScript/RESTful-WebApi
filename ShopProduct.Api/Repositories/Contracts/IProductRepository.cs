using ShopProduct.Api.Dtos;
using ShopProduct.Api.Entities;
using System.Threading.Tasks.Sources;

namespace ShopProduct.Api.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductAsync(int id);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task UpdateProductAsync(Product product);
    }
}
