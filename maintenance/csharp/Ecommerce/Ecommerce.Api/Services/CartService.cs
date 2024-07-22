namespace Ecommerce.Api.Services;

public class CartService : ICartService
{
    public Task<Cart> GetCart(Guid id, int version = -1)
    {
        throw new NotImplementedException();
    }

    public Task<Cart> CreateCart(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public Task<Cart> UpdateItem(Guid id, int version, CartItem item)
    {
        throw new NotImplementedException();
    }

    public Task<Cart> ApplyCoupon(Guid id, int version, Coupon coupon)
    {
        throw new NotImplementedException();
    }
}
