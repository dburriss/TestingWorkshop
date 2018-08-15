using System;
using System.Collections.Generic;

namespace CheckoutCS.Tests.Unit
{
    internal class CartBuilder
    {
        private List<ProductLine> _initInCart = new List<ProductLine>();

        public Cart Build()
        {
            var cart =  new Cart(_initInCart);
            return cart;
        }

        public static implicit operator Cart(CartBuilder builder) => builder.Build();

        internal CartBuilder Containing(params ProductLineBuilder[] productLineBuilders)
        {
            foreach (ProductLine cmd in productLineBuilders)
            {
                _initInCart.Add(cmd);
            }
            return this;
        }
    }
}