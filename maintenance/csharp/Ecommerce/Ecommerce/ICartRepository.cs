namespace Ecommerce;

public interface ICartRepository
{
    Task<Cart?> GetCart(Guid id);
    Task<Cart> CreateCart(Cart cart);
    Task<Cart> UpdateCart(Cart cart);
    Task DeleteCart(Guid id);
}
