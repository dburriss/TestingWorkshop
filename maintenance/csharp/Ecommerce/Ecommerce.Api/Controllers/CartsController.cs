using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("[controller]/{id:guid}")]
public class CartsController: ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ILogger<CartsController> _logger;
    
    public CartsController(ICartService cartService, ILogger<CartsController> logger)
    {
        _cartService = cartService;
        _logger = logger;
    }

    [HttpGet(Name = "GetCart")]
    public async Task<Cart> Get(Guid id)
    {
        return await _cartService.GetCart(id);
    }
    
    [HttpPut("{version}/items", Name = "UpdateItem")]
    public Task<Cart> UpdateItem(Guid id, int version, SetCartItem item)
    {
        return _cartService.UpdateItem(id, version, item);
    }
    
    [HttpPut("{version}/coupons", Name = "ApplyCoupon")]
    public Task<Cart> ApplyCoupon(Guid id, int version, string couponCode)
    {
        return _cartService.ApplyCoupon(id, version, couponCode);
    }
}
