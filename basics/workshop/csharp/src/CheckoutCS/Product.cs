using System;

namespace CheckoutCS
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public long Version { get; set; }
    }
}
