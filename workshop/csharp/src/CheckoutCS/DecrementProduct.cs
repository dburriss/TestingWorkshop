using System;

namespace CheckoutCS
{
    public class DecrementProduct
    {
        public DecrementProduct(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}