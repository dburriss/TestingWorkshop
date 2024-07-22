using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("[controller]/{id:guid}/{version:int}")]
public class CartController: ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ILogger<CartController> _logger;
    
    public CartController(ICartService cartService, ILogger<CartController> logger)
    {
        _cartService = cartService;
        _logger = logger;
    }

    [HttpGet(Name = "GetCart")]
    public async Task<Cart> Get(Guid id)
    {
        return await _cartService.GetCart(id);
    }
    
    [HttpPut("items", Name = "UpdateItem")]
    public Task<Cart> UpdateItem(Guid id, int version, CartItem item)
    {
        return _cartService.UpdateItem(id, version, item);
    }
    
    [HttpPut("coupons", Name = "ApplyCoupon")]
    public Task<Cart> ApplyCoupon(Guid id, int version, Coupon coupon)
    {
        return _cartService.ApplyCoupon(id, version, coupon);
    }
    
}
