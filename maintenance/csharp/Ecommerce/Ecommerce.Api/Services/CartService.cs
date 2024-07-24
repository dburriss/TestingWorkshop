namespace Ecommerce.Api.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductCatalogService _productCatalogService;
    private readonly ICouponService _couponService;

    public CartService(ICartRepository cartRepository, IProductCatalogService productCatalogService, ICouponService couponService)
    {
        _cartRepository = cartRepository;
        _productCatalogService = productCatalogService;
        _couponService = couponService;
    }

    public Task<Cart> GetCart(Guid customerId, int version = -1)
    {
        return _cartRepository.GetCart(customerId);
    }

    public async Task<Cart> CreateCart(Guid customerId)
    {
        var cart = new Cart
        {
            Coupons = new(),
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            CreatedAt = DateTimeOffset.UtcNow,
            Items = new(),
        };
        return await _cartRepository.CreateCart(cart);
    }

    public async Task<Cart> UpdateItem(Guid customerId, int version, SetCartItem item)
    {
        var cart = await _cartRepository.GetCart(customerId);
        if (cart == null)
        {
            cart = new Cart
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                CreatedAt = DateTimeOffset.UtcNow,
                Items = new(),
                Coupons = new()
            };
            await _cartRepository.CreateCart(cart);
        }
        // this should be enforced by the database
        if (cart.Version != version)
        {
            throw new Exception("Version mismatch");
        }
        var product = await _productCatalogService.GetProduct(item.ProductId);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        if(cart.Items.ContainsKey(item.ProductId))
        {
            cart.Items[item.ProductId] = cart.Items[item.ProductId].WithQuantity(cart.Items[item.ProductId].Quantity + item.Quantity);
            return await _cartRepository.UpdateCart(cart);
        }
        
        cart.Items[item.ProductId] = new CartItem(product!.Id, product.Name, product.Price, item.Quantity, product.ProductCategory);
        return await _cartRepository.UpdateCart(cart);
    }

    public async Task<Cart> ApplyCoupon(Guid id, int version, string couponCode)
    {
        var cart = await _cartRepository.GetCart(id);
        if (cart == null)
        {
            cart = await CreateCart(id);
        }
        if (cart.Version != version)
        {
            throw new Exception("Version mismatch");
        }
        var coupon = await _couponService.GetCoupon(couponCode);
        if (coupon == null)
        {
            throw new Exception("Invalid coupon");
        }
        cart.Coupons.Add(coupon);
        return await _cartRepository.UpdateCart(cart);
    }
}
