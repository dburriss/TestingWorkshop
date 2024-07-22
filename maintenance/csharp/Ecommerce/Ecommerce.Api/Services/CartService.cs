namespace Ecommerce.Api.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductService _productService;

    public CartService(ICartRepository cartRepository, IProductService productService)
    {
        _cartRepository = cartRepository;
        _productService = productService;
    }

    public Task<Cart> GetCart(Guid customerId, int version = -1)
    {
        return _cartRepository.GetCart(customerId);
    }

    public async Task<Cart> CreateCart(Guid customerId)
    {
        var cart = new Cart
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            CreatedAt = DateTimeOffset.UtcNow,
            Items = new List<CartItem>(),
            Coupons = new List<Coupon>(),
        };
        return await _cartRepository.CreateCart(cart);
    }

    public async Task<Cart> UpdateItem(Guid customerId, int version, AddCartItem item)
    {
        var cart = await _cartRepository.GetCart(customerId);
        if (cart == null)
        {
            cart = new Cart
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                CreatedAt = DateTimeOffset.UtcNow,
                Items = new List<CartItem>(),
                Coupons = new List<Coupon>(),
            };
            await _cartRepository.CreateCart(cart);
        }
        // this should be enforced by the database
        if (cart.Version != version)
        {
            throw new Exception("Version mismatch");
        }
        var product = await _productService.GetProduct(item.ProductId);
        cart.Items.Add(new CartItem(product.Id, product.Name, product.Price, item.Quantity));

        return await _cartRepository.UpdateCart(cart);
    }

    public Task<Cart> ApplyCoupon(Guid customerId, int version, Coupon coupon)
    {
        throw new NotImplementedException();
    }
}
