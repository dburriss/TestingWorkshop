namespace Ecommerce.Test;

public class CartTotalTests
{
    [Fact]
    public void WhenEmpty_ThenTotalPriceIsZero()
    {
        // Arrange
        var cart = new Cart
        {
            CreatedAt = DateTimeOffset.Now,
            CustomerId = Guid.NewGuid(),
            Items = new List<CartItem>(),
            Version = 0
        };

        // Act
        var totalPrice = cart.Total;

        // Assert
        Assert.Equal(0, totalPrice);
    }

    [Fact]
    public void WhenSingleItem_ThenTotalPriceIsItemPrice()
    {
        // Arrange
        var cart = new Cart
        {
            CreatedAt = DateTimeOffset.Now,
            CustomerId = Guid.NewGuid(),
            Items = new List<CartItem>
            {
                new CartItem(Guid.NewGuid(), "Product 1", 10, 1)
            },
            Version = 0
        };

        // Act
        var totalPrice = cart.Total;

        // Assert
        Assert.Equal(10, totalPrice);
    }

    [Fact]
    public void WhenMultipleItems_ThenTotalPriceIsSumOfItemPrices()
    {
        // Arrange
        var cart = new Cart
        {
            CreatedAt = DateTimeOffset.Now,
            CustomerId = Guid.NewGuid(),
            Items = new List<CartItem>
            {
                new CartItem(Guid.NewGuid(), "Product 1", 10, 1),
                new CartItem(Guid.NewGuid(), "Product 2", 20, 1)
            },
            Version = 0
        };

        // Act
        var totalPrice = cart.Total;

        // Assert
        Assert.Equal(30, totalPrice);
    }
}
