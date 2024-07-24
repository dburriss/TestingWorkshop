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
                new(Guid.NewGuid(), "Product 1", 10, 1, ProductCategory.Cameras)
            },
            Version = 0,
            Coupons = new()
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
                new(Guid.NewGuid(), "Product 1", 10, 1, ProductCategory.Cameras),
                new(Guid.NewGuid(), "Product 2", 20, 1, ProductCategory.Cameras)
            },
            Version = 0,
            Coupons = new()
        };

        // Act
        var totalPrice = cart.Total;

        // Assert
        Assert.Equal(30, totalPrice);
    }

    [Fact]
    public void WhenCartItemHas50PercentCoupon_TotalIsHalf()
    {
        // Arrange
        var cart = new Cart
        {
            CreatedAt = DateTimeOffset.Now,
            CustomerId = Guid.NewGuid(),
            Items = new List<CartItem>
            {
                new(Guid.NewGuid(), "Product 1", 100, 1, ProductCategory.Cameras),
            },
            Version = 0,
            Coupons = new List<Coupon>
            {
                new(
                    Guid.NewGuid(), 
                    DateTimeOffset.Now.AddDays(1), 
                    "50-OFF", 
                    50M, 
                    new() { ProductCategory.Cameras })
            }
        };

        // Act
        var totalPrice = cart.Total;

        // Assert
        Assert.Equal(50, totalPrice);
    }
}
