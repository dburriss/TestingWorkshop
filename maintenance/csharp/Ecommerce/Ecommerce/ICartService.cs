namespace Ecommerce;

public interface ICartService
{
    Task<Cart> GetCart(Guid customerId, int version = 0);
    Task<Cart> CreateCart(Guid customerId);
    Task<Cart> UpdateItem(Guid customerId, int version, AddCartItem item);
    Task<Cart> ApplyCoupon(Guid customerId, int version, Coupon coupon);
}
