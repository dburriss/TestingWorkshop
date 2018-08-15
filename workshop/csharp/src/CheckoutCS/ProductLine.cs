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
        private int quantity;

        public ProductLine(Guid id, string code, string name, string description, decimal amount, int quantity, long version)
        {
            this.id = id;
            this.code = code;
            this.name = name;
            this.description = description;
            this.amount = amount;
            this.version = version;
            this.quantity = quantity;
        }

        public Guid ProductId => id;
        public string ProductName => name;
        public decimal ProductAmount => amount;
        public int Quantity => quantity;

        internal void Increment()
        {
            quantity = quantity + 1;
        }

        internal void Decrement()
        {
            if(quantity > 0)
            {
                quantity = quantity - 1;
            }            
        }

        internal void SetQuantity(int quantity)
        {
            if(quantity < 0)
            {
                throw new ArgumentException("Quantity cannont be less than zero.", nameof(quantity));
            }
            this.quantity = quantity;
        }
    }
}