namespace Ecommerce;

public interface ITaxService
{
    Task<decimal> TaxPercentage(CustomerRef customer);
}
