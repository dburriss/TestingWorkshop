using Ecommerce.Api.Services;

using NSubstitute;

namespace Ecommerce.Tests;

public class CartServiceTests
{
    [Fact]
    public async Task WhenSetItem_ThenItemIsAddedToCart()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var cartRepository = Substitute.For<ICartRepository>();
        var productCatalogService = Substitute.For<IProductCatalogService>();
        productCatalogService
            .GetProduct(Arg.Any<Guid>())
            .Returns(new ProductRef(Guid.NewGuid(), "Product 1", ProductCategory.Cameras, 10));
        var cartService = new CartService(cartRepository, productCatalogService);

        var item = new SetCartItem(productId, 1);

        // Act
        var cart = await cartService.UpdateItem(customerId, item);

        // Assert
        Assert.Single(cart.Items);
        Assert.Equal(10, cart.Total);
    }
}
