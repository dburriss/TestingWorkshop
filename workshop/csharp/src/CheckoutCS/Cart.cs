using System;
using System.Collections.Generic;

namespace CheckoutCS
{
    public class Cart
    {
        private List<ProductLine> _productLines = new List<ProductLine>();
        public IEnumerable<ProductLine> ProductLines => _productLines;

        public void Handle(AddProduct cmd)
        {
            if(cmd == null) throw new ArgumentNullException(nameof(cmd));
            if (cmd.Id == Guid.Empty) throw new ArgumentException($"{nameof(cmd.Id)} must be a valid non-Empty Guid");
            if (string.IsNullOrEmpty(cmd.Code)) throw new ArgumentException($"{nameof(cmd.Id)} must be a valid non-Empty Guid");
            if (string.IsNullOrEmpty(cmd.Name)) throw new ArgumentException($"{nameof(cmd.Name)} cannot be empty");

            _productLines.Add(new ProductLine(cmd.Id, cmd.Code, cmd.Name, cmd.Description, cmd.Amount, cmd.Version));
        }

        public void Handle(RemoveProduct cmd)
        {
            if (cmd == null) throw new ArgumentNullException(nameof(cmd));
            if (cmd.Id == Guid.Empty) throw new ArgumentException($"{nameof(cmd.Id)} must be a valid non-Empty Guid");

            _productLines.RemoveAll(x => x.ProductId == cmd.Id);
        }
    }
}
