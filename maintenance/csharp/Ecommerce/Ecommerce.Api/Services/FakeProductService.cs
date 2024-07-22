namespace Ecommerce.Api.Services;

public class FakeProductService : IProductService
{
    public Task<ProductRef> GetProduct(Guid productId)
    {
        return Task.FromResult<ProductRef>(new ProductRef(productId, $"A product with ID {productId}", 100M));
    }
}
