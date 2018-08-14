using System;

namespace CheckoutCS.Tests.Unit
{
    internal class AddProductBuilder
    {
        private Guid id = Guid.Parse("62a8420a-83a5-4848-ba2e-2c1a761bc21b");
        private string code = "P0001";
        private string name = "Test Product";
        private string description = "Test Product Description is Awesome!";
        private decimal amount = 1.0m;
        private long version = 0;
        
        public AddProduct Build()
        {
            return new AddProduct(id, code, name, description, amount, version);
        }

        public static implicit operator AddProduct(AddProductBuilder builder) => builder.Build();

        internal AddProductBuilder WithId(Guid id)
        {
            this.id = id;
            return this;
        }

        internal AddProductBuilder WithCode(string code)
        {
            this.code = code;
            return this;
        }

        internal AddProductBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        internal AddProductBuilder WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        internal AddProductBuilder WithAmount(decimal amount)
        {
            this.amount = amount;
            return this;
        }

        internal AddProductBuilder WithVersion(long version)
        {
            this.version = version;
            return this;
        }
    }
}