using System;

namespace CheckoutCS
{
    public class RemoveProduct
    {
        public RemoveProduct(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}