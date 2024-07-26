namespace Ecommerce;

public interface ICartService
{
    Task<Cart> GetCart(Guid customerId, int version = 0);
    Task<Cart> CreateCart(Guid customerId);
    Task<Cart> UpdateItem(Guid customerId, SetCartItem item);
}
