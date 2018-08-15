using System;

namespace CheckoutCS
{
    public class IncrementProduct
    {
        public IncrementProduct(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}