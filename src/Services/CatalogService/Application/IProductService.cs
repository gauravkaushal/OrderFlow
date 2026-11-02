
using CatalogService.Domain;

namespace CatalogService.Application;

public interface IProductService
{
    Task<IReadOnlyList<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product> CreateAsync(string name, decimal price);

}


