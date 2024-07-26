namespace Ecommerce.Api.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductCatalogService _productCatalogService;

    public CartService(ICartRepository cartRepository, IProductCatalogService productCatalogService)
    {
        _cartRepository = cartRepository;
        _productCatalogService = productCatalogService;
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
        };
        return await _cartRepository.CreateCart(cart);
    }

    public async Task<Cart> UpdateItem(Guid customerId, SetCartItem item)
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
            };
            await _cartRepository.CreateCart(cart);
        }

        var product = await _productCatalogService.GetProduct(item.ProductId);
        cart.Items.Add(new CartItem(product!.Id, product.Name, product.Price, item.Quantity));

        return await _cartRepository.UpdateCart(cart);
    }
}
