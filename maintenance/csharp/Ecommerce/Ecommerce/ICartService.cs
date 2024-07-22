namespace Ecommerce;

public interface ICartService
{
    Task<Cart> GetCart(Guid id, int version = 0);
    Task<Cart> CreateCart(Guid customerId);
    Task<Cart> UpdateItem(Guid id, int version, CartItem item);
    Task<Cart> ApplyCoupon(Guid id, int version, Coupon coupon);
}
