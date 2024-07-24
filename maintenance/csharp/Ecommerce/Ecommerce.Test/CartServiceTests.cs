using Ecommerce.Api.Services;

using NSubstitute;

namespace Ecommerce.Test;

public class CartServiceTests
{
    [Fact]
    public async Task WhenAddItem_ThenItemIsInToCart()
    {
        // Arrange
        var setItem = new SetCartItem(Guid.NewGuid(), 1);
        var cartRepository = Substitute.For<ICartRepository>();
        cartRepository.UpdateCart(Arg.Any<Cart>()).Returns(c =>
        {
            var newCart = c.Arg<Cart>();
            newCart.Version++;
            return newCart;
        });
        var productService = Substitute.For<IProductCatalogService>();
        productService.GetProduct(setItem.ProductId)
            .Returns(new ProductRef(setItem.ProductId, "Product 1", ProductCategory.Cameras, 10));
        var couponService = Substitute.For<ICouponService>();
        var cartService = new CartService(cartRepository, productService, couponService);
        var customerId = Guid.NewGuid();

        // Act
        var cart = await cartService.UpdateItem(customerId, 0, setItem);

        // Assert
        Assert.NotNull(cart);
        Assert.Single(cart.Items);
    }
}
