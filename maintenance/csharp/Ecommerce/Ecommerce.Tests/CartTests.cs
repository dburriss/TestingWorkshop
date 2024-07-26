namespace Ecommerce.Tests;

public class CartTests
{
    private readonly Cart _cart;

    public CartTests()
    {
        _cart = new Cart();
    }
    [Fact]
    public void WhenEmpty_ThenTotalZero()
    {
        // Arrange
        Cart cart = A.Cart; 

        // Act
        var totalPrice = cart.Total;

        // Assert
        Assert.Equal(0, totalPrice);
    }
    
    [Fact]
    public void WhenSingleItem_ThenTotalIsItemPrice()
    {
        // Arrange
        _cart.Items = new List<CartItem>
        {
            new CartItem(Guid.NewGuid(), "Product 1", 10, 1)
        };

        // Act
        var totalPrice = _cart.Total;

        // Assert
        Assert.Equal(10, totalPrice);
    }
    
    [Fact]
    public void WhenMultipleItems_ThenTotalIsSumOfItemPrices()
    {
        // Arrange
        var cart = CreateCart();

        // Act
        var totalPrice = cart.Total;

        // Assert
        Assert.Equal(30, totalPrice);
    }

    [Fact]
    public void WhenMultipleQuantities_ThenTotalIsSumOfItemPricesTimesQuantity()
    {
        // Arrange
        var cart = new Cart
        {
            CreatedAt = DateTimeOffset.Now,
            CustomerId = Guid.NewGuid(),
            Items = new List<CartItem>
            {
                new CartItem(Guid.NewGuid(), "Product 1", 10, 2),
            },
        };

        // Act
        var totalPrice = cart.Total;

        // Assert
        Assert.Equal(20, totalPrice);
    }

    [Fact]
    public void WhenPriceIsFloatingPoint_ThenTotalIsNotRounded()
    {
        // Arrange
        var cart = new Cart
        {
            CreatedAt = DateTimeOffset.Now,
            CustomerId = Guid.NewGuid(),
            Items = new List<CartItem>
            {
                new CartItem(Guid.NewGuid(), "Product 1", 10.99m, 1)
            },
        };

        // Act
        var totalPrice = cart.Total;

        // Assert
        Assert.Equal(10.99m, totalPrice);
    }
    
    private static Cart CreateCart(params CartItem[] items)
    {
        return new Cart
        {
            CreatedAt = DateTimeOffset.Now,
            CustomerId = Guid.NewGuid(),
            Items = items.ToList(),
        };
    }

}

// a cart builder
public class CartBuilder
{
    private List<CartItem> _items = new();

    public CartBuilder WithItem(CartItem cartItem)
    {
        _items.Add(cartItem);
        return this;
    }

    public Cart Build()
    {
        var cart = new Cart
        {
            CreatedAt = DateTimeOffset.Now,
            CustomerId = Guid.NewGuid(),
            Items = _items,
        };
        return cart;
    }
    
    public static implicit operator Cart(CartBuilder builder) => builder.Build();
}

public static class A
{
    public static CartBuilder Cart => new();
}
