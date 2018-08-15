using System;
using Xunit;

namespace CheckoutCS.Tests.Unit
{
    public class CartLoggerTests
    {

        [Fact]
        public void Information_WhenProductAdded_LoggerIsCalled()
        {
            LoggerSpy logger = A.Logger;
            Cart cart = A.Cart.UsingLogger(logger);
            AddProduct cmd = A.AddProduct;
            cart.Handle(cmd);
            Assert.True(logger.InformationWasCalled);
        }

        [Fact]
        public void Information_WhenProductRemoved_LoggerIsCalled()
        {
            var id = Guid.NewGuid();
            LoggerSpy logger = A.Logger;
            Cart cart = A.Cart.Containing(A.ProductLine.WithId(id)).UsingLogger(logger);
            RemoveProduct cmd = A.RemoveProduct(id);
            cart.Handle(cmd);
            Assert.True(logger.InformationWasCalled);
        }

    }
}
