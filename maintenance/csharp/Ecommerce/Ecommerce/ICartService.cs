namespace Ecommerce;

public interface ICartService
{
    Task<Cart> GetCart(Guid customerId, int version = 0);
    Task<Cart> CreateCart(Guid customerId);
    Task<Cart> UpdateItem(Guid customerId, int version, SetCartItem item);
    Task<Cart> ApplyCoupon(Guid id, int version, string couponCode);
}
