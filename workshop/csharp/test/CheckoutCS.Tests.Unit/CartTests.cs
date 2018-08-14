using System;
using System.Linq;
using Xunit;

namespace CheckoutCS.Tests.Unit
{
    public class CartTests
    {
        [Fact]
        public void AddProduct_WithNull_ThrowsArgumentNullException()
        {
            //arrange
            var cart = Sut();
            AddProduct cmd = null;

            //act and assert
            Assert.Throws<ArgumentNullException>(() => cart.Handle(cmd));
        }


        [Fact]
        public void AddProduct_WithDefaultId_ThrowsArgumentException()
        {
            //arrange
            var cart = Sut();
            AddProduct cmd = A.AddProduct.WithId(Guid.Empty);

            //act and assert
            Assert.Throws<ArgumentException>(() => cart.Handle(cmd));
        }

        [Fact]
        public void AddProduct_WithMissingName_ThrowsArgumentException()
        {
            //arrange
            var cart = Sut();
            var cmd = A.AddProduct.WithName("");

            //act and assert
            Assert.Throws<ArgumentException>(() => cart.Handle(cmd));
        }

        [Fact]
        public void AddProduct_WithValidCommand_AddsAProductLineToCart()
        {
            //arrange
            var cart = Sut();
            var cmd = A.AddProduct;

            //act
            cart.Handle(cmd);

            //assert
            Assert.NotEmpty(cart.ProductLines);
        }

        [Fact]
        public void AddProduct_WithValidCommand_AddsCorrectProductLineToCart()
        {
            //arrange
            var cart = Sut();
            AddProduct cmd = A.AddProduct;

            //act
            cart.Handle(cmd);

            //assert
            Assert.Equal(cmd.Id, cart.ProductLines.First().ProductId);
            Assert.Equal(cmd.Name, cart.ProductLines.First().ProductName);
            Assert.Equal(cmd.Amount, cart.ProductLines.First().ProductAmount);
        }

        private Cart Sut()
        {
            return new Cart();
        }
    }
}
