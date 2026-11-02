
using CatalogService.Domain;
using CatalogService.Infrastructure;

namespace CatalogService.Application;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IProductRepository productRepository,
        ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<Product> CreateAsync(
        string name,
        decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Product name cannot be empty.",
                nameof(name));
        }

        if (price <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(price),
                "Product price must be greater than zero.");
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Price = price,
            IsActive = true
        };

        await _productRepository.AddAsync(product);

        _logger.LogInformation(
            "Created product {ProductId} with name {ProductName}",
            product.Id,
            product.Name);

        return product;
    }

    public Task<IReadOnlyList<Product>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all products");

        return _productRepository.GetAllAsync();
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        _logger.LogInformation(
            "Retrieving product {ProductId}",
            id);

        return _productRepository.GetByIdAsync(id);
    }
}
