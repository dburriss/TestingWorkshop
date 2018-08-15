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
            Cart cart = A.Cart;
            AddProduct cmd = null;

            //act and assert
            Assert.Throws<ArgumentNullException>(() => cart.Handle(cmd));
        }


        [Fact]
        public void AddProduct_WithDefaultId_ThrowsArgumentException()
        {
            //arrange
            Cart cart = A.Cart;
            AddProduct cmd = A.AddProduct.WithId(Guid.Empty);

            //act and assert
            Assert.Throws<ArgumentException>(() => cart.Handle(cmd));
        }

        [Fact]
        public void AddProduct_WithMissingName_ThrowsArgumentException()
        {
            //arrange
            Cart cart = A.Cart;
            var cmd = A.AddProduct.WithName("");

            //act and assert
            Assert.Throws<ArgumentException>(() => cart.Handle(cmd));
        }

        [Fact]
        public void AddProduct_WithValidCommand_AddsAProductLineToCart()
        {
            //arrange
            Cart cart = A.Cart;
            var cmd = A.AddProduct;

            //act
            cart.Handle(cmd);

            //assert
            Assert.NotEmpty(cart.ProductLines);
        }

        [Fact]
        public void AddProduct_WithValidCommand_AddsCorrectProductLineToCart()
        {
            Cart cart = A.Cart;
            AddProduct cmd = A.AddProduct;

            cart.Handle(cmd);

            Assert.Equal(cmd.Id, cart.ProductLines.First().ProductId);
            Assert.Equal(cmd.Name, cart.ProductLines.First().ProductName);
            Assert.Equal(cmd.Amount, cart.ProductLines.First().ProductAmount);
        }

        [Fact]
        public void RemoveProduct_WithValidCommand_RemovesCorrectProductLineToCart()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            Cart cart = A.Cart.Containing(A.ProductLine.WithId(id1), A.ProductLine.WithId(id2));
            RemoveProduct cmd = A.RemoveProduct(id1);

            cart.Handle(cmd);

            Assert.Equal(id2, cart.ProductLines.First().ProductId);
        }

        [Fact]
        public void AddProduct_WithExistingProduct_IncrementsProductLine()
        {
            var id1 = Guid.NewGuid();
            Cart cart = A.Cart.Containing(A.ProductLine.WithId(id1));
            AddProduct cmd = A.AddProduct.WithId(id1);

            cart.Handle(cmd);

            Assert.Equal(2, cart.ProductLines.First().Quantity);
        }

        [Fact]
        public void IncrementProduct_WithExistingProduct_IncrementsProductLine()
        {
            var id1 = Guid.NewGuid();
            Cart cart = A.Cart.Containing(A.ProductLine.WithId(id1));
            var cmd = A.IncrementProduct(id1);

            cart.Handle(cmd);

            Assert.Equal(2, cart.ProductLines.First().Quantity);
        }

        [Fact]
        public void IncrementProduct_WithNonExistingProduct_ThrowsInvalidOperation()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            Cart cart = A.Cart.Containing(A.ProductLine.WithId(id1));
            var cmd = A.IncrementProduct(id2);

            Assert.Throws<InvalidOperationException>(() => cart.Handle(cmd));
        }

        [Fact]
        public void DecrementProduct_WithNonExistingProduct_ThrowsInvalidOperation()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            Cart cart = A.Cart.Containing(A.ProductLine.WithId(id1));
            var cmd = A.DecrementProduct(id2);

            Assert.Throws<InvalidOperationException>(() => cart.Handle(cmd));
        }

        [Fact]
        public void DecrementProduct_WithExistingProduct_DecrementsProductLine()
        {
            var id1 = Guid.NewGuid();
            Cart cart = A.Cart.Containing(A.ProductLine.WithId(id1));
            var cmd = A.DecrementProduct(id1);

            cart.Handle(cmd);

            Assert.Equal(0, cart.ProductLines.First().Quantity);
        }

        [Fact]
        public void DecrementProduct_WithZeroQuantity_DoesNotGoBelowZero()
        {
            var id1 = Guid.NewGuid();
            Cart cart = A.Cart.Containing(A.ProductLine.WithId(id1).WithQuantity(0));
            var cmd = A.DecrementProduct(id1);

            cart.Handle(cmd);

            Assert.Equal(0, cart.ProductLines.First().Quantity);
        }

        [Fact]
        public void SetProductQuantity_With5_CartContains5()
        {
            var id1 = Guid.NewGuid();
            Cart cart = A.Cart.Containing(A.ProductLine.WithId(id1).WithQuantity(5));
            var cmd = A.SetProductQuantity(id1, 5);

            cart.Handle(cmd);

            Assert.Equal(5, cart.ProductLines.First().Quantity);
        }

        [Fact]
        public void SetProductQuantity_WithNegative_ThrowsInvalidArgument()
        {
            var id1 = Guid.NewGuid();
            Cart cart = A.Cart.Containing(A.ProductLine.WithId(id1).WithQuantity(5));
            var cmd = A.SetProductQuantity(id1, -1);

            Assert.Throws<ArgumentException>(() => cart.Handle(cmd));
        }
    }
}
