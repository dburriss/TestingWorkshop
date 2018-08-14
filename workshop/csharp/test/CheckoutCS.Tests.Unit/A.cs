using System;

namespace CheckoutCS.Tests.Unit
{
    internal static class A
    {
        internal static AddProductBuilder AddProduct => new AddProductBuilder();
        internal static RemoveProduct RemoveProduct(Guid id) => new RemoveProduct(id);
        internal static CartBuilder Cart => new CartBuilder();

    }
}
