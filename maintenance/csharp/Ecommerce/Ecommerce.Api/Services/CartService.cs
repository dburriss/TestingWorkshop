namespace Ecommerce.Api.Services;

public class CartService: ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly ICouponService _couponService;
    private readonly ICustomerService _customerService;
    private readonly IProductCatalogService _productCatalogService;
    private readonly ITaxService _taxService;

    public CartService(
        ICartRepository cartRepository,
        IProductCatalogService productCatalogService,
        ICouponService couponService,
        ICustomerService customerService,
        ITaxService taxService)
    {
        _cartRepository = cartRepository;
        _productCatalogService = productCatalogService;
        _couponService = couponService;
        _customerService = customerService;
        _taxService = taxService;
    }

    public Task<Cart> GetCart(Guid customerId, int version = -1)
    {
        return _cartRepository.GetCart(customerId);
    }

    public async Task<Cart> CreateCart(Guid customerId)
    {
        var cart = new Cart
        {
            Coupons = new List<Coupon>(),
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            CreatedAt = DateTimeOffset.UtcNow,
            Items = new Dictionary<Guid, CartItem>()
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
                Items = new Dictionary<Guid, CartItem>(),
                Coupons = new List<Coupon>()
            };
            await _cartRepository.CreateCart(cart);
        }

        // this should be enforced by the database
        if (cart.Version != version) throw new Exception("Version mismatch");
        var product = await _productCatalogService.GetProduct(item.ProductId);
        if (product == null) throw new Exception("Product not found");
        var customer = await _customerService.GetCustomer(customerId);
        var taxRate = await _taxService.TaxPercentage(customer!);
        cart.TaxRate = taxRate;
        if (cart.Items.ContainsKey(item.ProductId))
        {
            cart.Items[item.ProductId] = cart.Items[item.ProductId]
                .WithQuantity(cart.Items[item.ProductId].Quantity + item.Quantity);
            return await _cartRepository.UpdateCart(cart);
        }

        cart.Items[item.ProductId] =
            new CartItem(product!.Id, product.Name, product.Price, item.Quantity, product.ProductCategory);
        return await _cartRepository.UpdateCart(cart);
    }

    public async Task<Cart> ApplyCoupon(Guid id, int version, string couponCode)
    {
        var cart = await _cartRepository.GetCart(id);
        if (cart == null) cart = await CreateCart(id);
        if (cart.Version != version) throw new Exception("Version mismatch");
        var coupon = await _couponService.GetCoupon(couponCode);
        if (coupon == null) throw new Exception("Invalid coupon");
        cart.Coupons.Add(coupon);
        return await _cartRepository.UpdateCart(cart);
    }
}
