using System.Collections.Concurrent;
using System.Net.Http.Headers;
using CatalogService.Domain;
using Microsoft.VisualBasic;

namespace CatalogService.Infrastructure;

public class InMemoryProductRepository : IProductRepository
{
    //current currentDictionary provide thread saftey dictionary operation 
    //but does not gurrantees thread sae modification of the objects stored inside it. 
  private readonly ConcurrentDictionary<Guid, Product> _products = new();

    public Task AddAsync(Product product)
    {
        if(_products.TryAdd(product.Id, product))
        {
            throw new InvalidOperationException(
                $"Product with ID {product.Id} already exists");
        }
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        IReadOnlyList<Product> products = _products.Values.ToList();
        return await Task.FromResult(products);
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        _products.TryGetValue(id, out var product);
        return await Task.FromResult(product);
    }   

}
