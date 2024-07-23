namespace Ecommerce;

public interface ICouponService
{
    Task<Coupon?> GetCoupon(string couponCode);
}
public record Coupon(Guid Id, DateTimeOffset ExpiresAt, string Code, decimal Discount, List<ProductCategory> ValidFor);
