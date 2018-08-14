using System;

namespace CheckoutCS
{
    public class AddProduct
    {
        public AddProduct(Guid id, string code, string name, string description, decimal amount, long version)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            Amount = amount;
            Version = version;
        }

        public Guid Id { get; }
        public string Code { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Amount { get; }
        public long Version { get; }
    }

}
