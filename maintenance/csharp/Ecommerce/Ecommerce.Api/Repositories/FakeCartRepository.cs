namespace Ecommerce.Api.Repositories;

// TODO: Replace with PostgreSQL implementation and migrations
public class FakeCartRepository : ICartRepository
{
    private static Dictionary<Guid, Cart> _carts = new();
    public Task<Cart?> GetCart(Guid id)
    {
        if (_carts.TryGetValue(id, out var cart))
        {
            return Task.FromResult(cart)!;
        }
        return Task.FromResult<Cart>(null);
    }

    public Task<Cart> CreateCart(Cart cart)
    {
        _carts.Add(cart.CustomerId, cart);
        return Task.FromResult(cart);
    }

    public Task<Cart> UpdateCart(Cart cart)
    {
        if (_carts.ContainsKey(cart.CustomerId))
        {
            _carts[cart.CustomerId] = cart;
        }
        else
        {
            _carts[cart.CustomerId] = cart;
        }
        return Task.FromResult(cart);
    }

    public Task DeleteCart(Guid id)
    {
        if (_carts.ContainsKey(id))
        {
            _carts.Remove(id);
        }
        return Task.CompletedTask;
    }
}
