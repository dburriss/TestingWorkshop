namespace Ecommerce;

public interface IProductCatalogService 
{
    Task<ProductRef?> GetProduct(Guid productId);
}
public record ProductRef(Guid Id, string Name, ProductCategory ProductCategory, decimal Price);
public enum ProductCategory
{
    ComputerGoods,
    WhiteGoods,
    MobilePhones,
    PowerTools,
    HomeAppliances,
    AudioVideo,
    Cameras,
    Gaming,
    Other
}
