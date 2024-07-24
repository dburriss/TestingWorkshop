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
            Items = new(),
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
        var productId = Guid.NewGuid();
        var cart = new Cart
        {
            CreatedAt = DateTimeOffset.Now,
            CustomerId = Guid.NewGuid(),
            Items = new()
            {
                {productId, new(productId, "Product 1", 10, 1, ProductCategory.Cameras)}
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
        var productId = Guid.NewGuid();
        var productId2 = Guid.NewGuid();
        var cart = new Cart
        {
            CreatedAt = DateTimeOffset.Now,
            CustomerId = Guid.NewGuid(),
            Items = new()
            {
                {productId, new(productId, "Product 1", 10, 1, ProductCategory.Cameras)},
                {productId2 ,new(productId2, "Product 2", 20, 1, ProductCategory.Cameras)}
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
        var productId = Guid.NewGuid();
        var cart = new Cart
        {
            CreatedAt = DateTimeOffset.Now,
            CustomerId = Guid.NewGuid(),
            Items = new()
            {
                {productId, new(productId, "Product 1", 100, 1, ProductCategory.Cameras)},
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
    
    [Fact]
    public void WhenMultipleQuantityOfItem_ThenTotalPriceIsItemPriceMultipliedByQuantiy()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var cart = new Cart
        {
            CreatedAt = DateTimeOffset.Now,
            CustomerId = Guid.NewGuid(),
            Items = new()
            {
                {productId, new(productId, "Product 1", 10, 2, ProductCategory.Cameras)}
            },
            Version = 0,
            Coupons = new()
        };

        // Act
        var totalPrice = cart.Total;

        // Assert
        Assert.Equal(20, totalPrice);
    }
}
