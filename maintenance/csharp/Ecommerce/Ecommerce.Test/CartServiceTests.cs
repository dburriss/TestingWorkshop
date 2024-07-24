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
            newCart.Version = newCart.Version++;
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
    
    [Fact]
    public async Task WhenAddSameItem_ThenItemQuantityIsIncremented()
    {
        // Arrange
        Cart store = null;
        var productId = Guid.NewGuid();
        var setItem = new SetCartItem(productId, 1);
        var cartRepository = Substitute.For<ICartRepository>();
        cartRepository.GetCart(Arg.Any<Guid>()).Returns(c => store);
        cartRepository.UpdateCart(Arg.Any<Cart>()).Returns(c =>
        {
            var newCart = c.Arg<Cart>();
            newCart.Version = newCart.Version++;
            store = newCart;
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
        var newCart = await cartService.UpdateItem(customerId, cart.Version, setItem);

        // Assert
        Assert.Equal(2u, newCart.Items[productId].Quantity);
    }
    
    
}
