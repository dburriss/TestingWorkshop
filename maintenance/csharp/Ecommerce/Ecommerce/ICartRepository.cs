namespace Ecommerce;

public interface ICartRepository
{
    Task<Cart> GetCart(Guid id);
    Task CreateCart(Cart cart);
    Task UpdateCart(Cart cart);
    Task DeleteCart(Guid id);
}
