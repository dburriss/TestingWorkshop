using System;

namespace CheckoutCS
{
    public class SetProductQuantity
    {
        public SetProductQuantity(Guid id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        public Guid Id { get; }
        public int Quantity { get; }
    }
}