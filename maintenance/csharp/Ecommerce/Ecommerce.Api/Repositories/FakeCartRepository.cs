namespace Ecommerce.Api.Repositories;

// TODO: Replace with PostgreSQL implementation and migrations
public class FakeCartRepository : ICartRepository
{
    private static Dictionary<Guid, Cart> _carts = new();
    public Task<Cart> GetCart(Guid id)
    {
        if (_carts.ContainsKey(id))
        {
            return Task.FromResult(_carts[id]);
        }
        throw new KeyNotFoundException();
    }

    public Task CreateCart(Cart cart)
    {
        _carts.Add(cart.Id, cart);
        return Task.CompletedTask;
    }

    public Task UpdateCart(Cart cart)
    {
        if (_carts.ContainsKey(cart.Id))
        {
            _carts[cart.Id] = cart;
        }
        else
        {
            _carts[cart.Id] = cart;
        }
        
        return Task.CompletedTask;
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
