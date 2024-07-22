using System.Net.Http.Json;

using Microsoft.AspNetCore.Mvc.Testing;

namespace Ecommerce.Api.Test.E2e;

// test api
public class GivenACustomer : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly Guid _customerId;
    private readonly WebApplicationFactory<Program> _factory;

    public GivenACustomer(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _customerId = Guid.NewGuid();
    }
    
    [Fact]
    public async Task WhenAddsItem_ThenItemIsInCart()
    {
        var client = _factory.CreateClient();
        // generate a known product id
        var productId = Guid.Parse("a0b0e7b4-0b1b-4b3c-8d5e-6f7a8b9c0d1e");
        var item = new AddCartItem(productId, 1);
        var response = await client.PutAsJsonAsync($"carts/{_customerId}/0/items", item);
        response.EnsureSuccessStatusCode();
        var cart = await client.GetFromJsonAsync<Cart>($"carts/{_customerId}");
        Assert.Contains(productId, cart!.Items.Select(i => i.ProductId));
        Assert.Equal(100M, cart.Total);
    }
}
