using System;

namespace CheckoutCS.Tests.Unit
{
    internal static class A
    {
        internal static ProductLineBuilder ProductLine => new ProductLineBuilder();
        internal static AddProductBuilder AddProduct => new AddProductBuilder();
        internal static RemoveProduct RemoveProduct(Guid id) => new RemoveProduct(id);
        internal static CartBuilder Cart => new CartBuilder();
        internal static IncrementProduct IncrementProduct(Guid id) => new IncrementProduct(id);
        internal static DecrementProduct DecrementProduct(Guid id) => new DecrementProduct(id);
        internal static SetProductQuantity SetProductQuantity(Guid id, int quantity) => new SetProductQuantity(id, quantity);
    }
}
