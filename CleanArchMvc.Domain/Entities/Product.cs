﻿using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string? ExtensionImage { get; private set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Product(string name, string description, decimal price, int stock, string extensionImage)
        {
            ValidateDomain(name, description, price, stock, extensionImage);
        }

        public Product(int id, string name, string description, decimal price, int stock, string extensionImage)
        {
            ValidateId(id);

            ValidateDomain(name, description, price, stock, extensionImage);
        }

        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidateDomain(name, description, price, stock, image);
            CategoryId = categoryId;
        }

        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            ValidateName(name);

            ValidateDescription(description);

            ValidatePrice(price);

            ValidateStock(stock);

            ValidateExtensionImage(image);
        }

        private void ValidateId(int id)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value, need to be largest to 0!");
            Id = id;
        }

        private void ValidateName(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                                            "Invalid, name is required!");
            DomainExceptionValidation.When(name.Length < 3,
                                            "Invalid name, too short, minimum is 3 characters!");
            Name = name;
        }

        private void ValidateDescription(string description)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(description),
                                            "Invalid, description is required!");
            DomainExceptionValidation.When(description.Length < 5,
                                            "Invalid description, too short, minimum is 5 characters!");
            Description = description;
        }

        private void ValidatePrice(decimal price)
        {
            DomainExceptionValidation.When(price < 0, "Invalid price value, need to be largest to 0!");
            Price = price;
        }

        private void ValidateStock(int stock)
        {
            DomainExceptionValidation.When(stock < 0, "Invalid stock value, need to be largest to 0!");
            Stock = stock;
        }

        private void ValidateExtensionImage(string image)
        {
            DomainExceptionValidation.When(image?.Length > 250, "Invalid extension image, too long, maximum 250 characters!");
            ExtensionImage = image;
        }
    }
}
