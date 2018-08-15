using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutCS
{
    public class Cart
    {
        private List<ProductLine> _productLines = new List<ProductLine>();
        public IEnumerable<ProductLine> ProductLines => _productLines;

        public Cart(IEnumerable<ProductLine> productLines)
        {
            _productLines = productLines.ToList();
        }

        public void Handle(AddProduct cmd)
        {
            if(cmd == null) throw new ArgumentNullException(nameof(cmd));
            if (cmd.Id == Guid.Empty) throw new ArgumentException($"{nameof(cmd.Id)} must be a valid non-Empty Guid");
            if (string.IsNullOrEmpty(cmd.Code)) throw new ArgumentException($"{nameof(cmd.Id)} must be a valid non-Empty Guid");
            if (string.IsNullOrEmpty(cmd.Name)) throw new ArgumentException($"{nameof(cmd.Name)} cannot be empty");

            if (_productLines.Any(x => x.ProductId == cmd.Id))
            {
                IncrementProductLine(cmd.Id);
            }
            else
            {
                _productLines.Add(new ProductLine(cmd.Id, cmd.Code, cmd.Name, cmd.Description, cmd.Amount, 0, cmd.Version));
            }
        }

        public void Handle(RemoveProduct cmd)
        {
            if (cmd == null) throw new ArgumentNullException(nameof(cmd));
            if (cmd.Id == Guid.Empty) throw new ArgumentException($"{nameof(cmd.Id)} must be a valid non-Empty Guid");

            _productLines.RemoveAll(x => x.ProductId == cmd.Id);
        }

        public void Handle(IncrementProduct cmd)
        {
            IncrementProductLine(cmd.Id);
        }

        public void Handle(DecrementProduct cmd)
        {
            DecrementProductLine(cmd.Id);
        }

        private void IncrementProductLine(Guid id)
        {
            var line = _productLines.Find(x => x.ProductId == id);
            if (line != null)
            {
                line.Increment();
            }
            else
            {
                throw new InvalidOperationException($"Could not increment product with ID {id} as it was not found in the cart.");
            }
        }

        private void DecrementProductLine(Guid id)
        {
            var line = _productLines.Find(x => x.ProductId == id);
            if (line != null)
            {
                line.Decrement();
            }
            else
            {
                throw new InvalidOperationException($"Could not decrement product with ID {id} as it was not found in the cart.");
            }
        }
    }
}
