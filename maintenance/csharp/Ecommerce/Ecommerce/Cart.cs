namespace Ecommerce;

public class Cart
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Dictionary<Guid, CartItem> Items { get; set; }
    public decimal Total => CalculateTotal();
    public int Version { get; set; }
    public List<Coupon> Coupons { get; set; }
    public decimal TaxRate { get; set; } = 0;
    
    private decimal CalculateTotal()
    {
        return Items.Values.Sum(item => ApplyCouponToItem(item) * item.Quantity);
    }

    private decimal ApplyCouponToItem(CartItem item)
    {
        var validCoupons = Coupons.Where(c => c.ExpiresAt >= DateTimeOffset.Now && c.ValidFor.Contains(item.Category)).ToList();
        var price = item.Price * (1 - validCoupons.Sum(c => c.Discount) / 100);
        price = price * (1 + TaxRate / 100);
        return price;
    }
}

public record CartItem(
    Guid ProductId,
    string Name,
    decimal Price,
    uint Quantity,
    ProductCategory Category)
{
    public CartItem WithQuantity(uint quantity) => this with { Quantity = quantity };
};
public record SetCartItem(Guid ProductId, uint Quantity);
