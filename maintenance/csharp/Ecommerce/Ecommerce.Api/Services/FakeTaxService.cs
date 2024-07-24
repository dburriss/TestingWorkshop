namespace Ecommerce.Api.Services;

public class FakeTaxService : ITaxService
{
    private static readonly Random _random = new Random();
    public Task<decimal> TaxPercentage(CustomerRef customer)
    {
        Jitter();
        if (!customer.IsBusinessCustomer && customer.CountryCode == "US")
        {
            return Task.FromResult(10m);
        }
        if(!customer.IsBusinessCustomer)
        {
            return Task.FromResult(0m);
        }
        // for business customers
        return Task.FromResult(customer.CountryCode switch
        {
            "NL" => 20m,
            "GB" => 20m,
            "DE" => 19m,
            "US" => 0m,
            _ => customer.IsBusinessCustomer ? 0.00m : 0.21m
        });
    }
    
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
