namespace Ecommerce.Api.Services;

using System;
using System.Threading;
using System.Collections.Generic;

public class FakeCustomerService : ICustomerService
{
    // Generate known customer data
    private static readonly Dictionary<Guid, CustomerRef> _customer = new()
    {
        { Guid.Parse("a0a0a0a0-a0a0-a0a0-a0a0-a0a0a0a0a0a0"), new CustomerRef(Guid.Parse("a0a0a0a0-a0a0-a0a0-a0a0-a0a0a0a0a0a0"), "Alice", "NL", false) },
        { Guid.Parse("b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1"), new CustomerRef(Guid.Parse("b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1"), "Bob", "GB", false) },
        { Guid.Parse("c2c2c2c2-c2c2-c2c2-c2c2-c2c2c2c2c2c2"), new CustomerRef(Guid.Parse("c2c2c2c2-c2c2-c2c2-c2c2-c2c2c2c2c2c2"), "Charlie", "DE", false) },
        { Guid.Parse("d3d3d3d3-d3d3-d3d3-d3d3-d3d3d3d3d3d3"), new CustomerRef(Guid.Parse("d3d3d3d3-d3d3-d3d3-d3d3-d3d3d3d3d3d3"), "David", "US", false) },
        { Guid.Parse("e4e4e4e4-e4e4-e4e4-e4e4-e4e4e4e4e4e4"), new CustomerRef(Guid.Parse("e4e4e4e4-e4e4-e4e4-e4e4-e4e4e4e4e4e4"), "Eve", "NL", true) },
    };
    private readonly Random _random = new ();
    public Task<CustomerRef?> GetCustomer(Guid customerId)
    {
        Jitter();
        return Task.FromResult(_customer.GetValueOrDefault(customerId));
    }
    
    //jitter
    private void Jitter()
    {
        var jitter = _random.Next(0, 1000);
        Thread.Sleep(jitter);
        if (jitter > 900)
        {
            throw new Exception("Random connection error");
        }
    }
}
