namespace Ecommerce;

public interface IProductCatalogService
{
    Task<ProductRef?> GetProduct(Guid productId);
    Task<Coupon?> GetCoupon(string couponCode);
}
public record ProductRef(Guid Id, string Name, ProductCategory ProductCategory, decimal Price);
public record Coupon(Guid Id, DateTimeOffset ExpiresAt, string Code, decimal Discount, List<ProductCategory> ValidFor);
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
