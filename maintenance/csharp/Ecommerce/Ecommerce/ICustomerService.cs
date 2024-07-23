namespace Ecommerce;

public interface ICustomerService
{
    Task<CustomerRef?> GetCustomer(Guid customerId);
}

public record CustomerRef(Guid CustomerId, string Name, string CountryCode, bool IsBusinessCustomer);
