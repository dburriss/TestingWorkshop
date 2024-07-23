namespace Ecommerce.Api.Services;

public class FakeProductCatalogService : IProductCatalogService
{
    // Dictionary of fake ProductRefs
    private static readonly Dictionary<Guid, ProductRef> _products = new()
    {
        // 1 from each category
        { Guid.Parse("00000000-0000-0000-0000-000000000001"), new ProductRef(Guid.Parse("00000000-0000-0000-0000-000000000001"), "Product 1", ProductCategory.ComputerGoods, 100M) },
        { Guid.Parse("00000000-0000-0000-0000-000000000002"), new ProductRef(Guid.Parse("00000000-0000-0000-0000-000000000002"), "Product 2", ProductCategory.WhiteGoods, 200M) },
        { Guid.Parse("00000000-0000-0000-0000-000000000003"), new ProductRef(Guid.Parse("00000000-0000-0000-0000-000000000003"), "Product 3", ProductCategory.MobilePhones, 300M) },
        { Guid.Parse("00000000-0000-0000-0000-000000000004"), new ProductRef(Guid.Parse("00000000-0000-0000-0000-000000000004"), "Product 4", ProductCategory.PowerTools, 400M) },
        { Guid.Parse("00000000-0000-0000-0000-000000000005"), new ProductRef(Guid.Parse("00000000-0000-0000-0000-000000000005"), "Product 5", ProductCategory.HomeAppliances, 500M) },
        { Guid.Parse("00000000-0000-0000-0000-000000000006"), new ProductRef(Guid.Parse("00000000-0000-0000-0000-000000000006"), "Product 6", ProductCategory.AudioVideo, 600M) },
        { Guid.Parse("00000000-0000-0000-0000-000000000007"), new ProductRef(Guid.Parse("00000000-0000-0000-0000-000000000007"), "Product 7", ProductCategory.Cameras, 700M) },
        { Guid.Parse("00000000-0000-0000-0000-000000000008"), new ProductRef(Guid.Parse("00000000-0000-0000-0000-000000000008"), "Product 8", ProductCategory.Gaming, 800M) },
        { Guid.Parse("00000000-0000-0000-0000-000000000009"), new ProductRef(Guid.Parse("00000000-0000-0000-0000-000000000009"), "Product 9", ProductCategory.Other, 900M) },
    };
    // Generate Hardcoded Coupons dictionary by code
    private static readonly Dictionary<string, Coupon> _coupons = new()
    {
        // generate random coupons
        { "10OFF", new Coupon(Guid.NewGuid(), DateTimeOffset.UtcNow.AddDays(7), "5OFF", 5M, new List<ProductCategory> { ProductCategory.ComputerGoods, ProductCategory.WhiteGoods }) },
        { "20OFF", new Coupon(Guid.NewGuid(), DateTimeOffset.UtcNow.AddDays(7), "10OFF", 10M, new List<ProductCategory> { ProductCategory.MobilePhones, ProductCategory.PowerTools }) },
        { "30OFF", new Coupon(Guid.NewGuid(), DateTimeOffset.UtcNow.AddDays(7), "15OFF", 150M, new List<ProductCategory> { ProductCategory.HomeAppliances, ProductCategory.AudioVideo }) },
        { "40OFF", new Coupon(Guid.NewGuid(), DateTimeOffset.UtcNow.AddDays(7), "20OFF", 200M, new List<ProductCategory> { ProductCategory.Cameras, ProductCategory.Gaming }) },
        { "50OFF", new Coupon(Guid.NewGuid(), DateTimeOffset.UtcNow.AddDays(7), "50OFF", 50M, new List<ProductCategory> { ProductCategory.Other }) },
        // expired
        { "EXPIRED", new Coupon(Guid.NewGuid(), DateTimeOffset.UtcNow.AddDays(-7), "EXPIRED", 50M, new List<ProductCategory> { ProductCategory.Other }) },
    };
    private readonly Random _random = new();
    public Task<ProductRef?> GetProduct(Guid productId)
    {
        // add some randomness to the response
        Jitter();
        // lookup product by id or return null
        return Task.FromResult(_products.GetValueOrDefault(productId));
    }

    private void Jitter()
    {
        var random = _random.Next(0, 1000);
        Thread.Sleep(random);
        if(random > 900)
        {
            throw new Exception("Random connection error");
        }
    }

    public Task<Coupon?> GetCoupon(string couponCode)
    {
        throw new NotImplementedException();
    }
}
