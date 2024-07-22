namespace Ecommerce;

public interface IProductService
{
    Task<ProductRef> GetProduct(Guid productId);
}
