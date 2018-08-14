using System;
using System.Collections.Generic;

namespace CheckoutCS
{
    public class ProductLine
    {
        private readonly Guid id;
        private readonly string code;
        private readonly string name;
        private readonly string description;
        private readonly decimal amount;
        private readonly long version;

        public ProductLine(Guid id, string code, string name, string description, decimal amount, long version)
        {
            this.id = id;
            this.code = code;
            this.name = name;
            this.description = description;
            this.amount = amount;
            this.version = version;
        }

        public Guid ProductId => id;
        public string ProductName => name;
        public decimal ProductAmount => amount;
    }
}