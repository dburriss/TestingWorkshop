using System;
using System.Collections.Generic;

namespace CheckoutCS.Tests.Unit
{
    internal class CartBuilder
    {
        private List<AddProduct> _addToCart = new List<AddProduct>();

        public Cart Build()
        {
            var cart =  new Cart();
            foreach (var cmd in _addToCart)
            {
                cart.Handle(cmd);
            }
            return cart;
        }

        public static implicit operator Cart(CartBuilder builder) => builder.Build();

        internal CartBuilder Add(params AddProductBuilder[] addProductBuilders)
        {
            foreach (AddProduct cmd in addProductBuilders)
            {
                _addToCart.Add(cmd);
            }
            return this;
        }
    }
}