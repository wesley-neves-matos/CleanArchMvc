using CleanArchMvc.Domain.Validation;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Xml.Linq;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            ValidateId(id);

            ValidateDomain(name, description, price, stock, image);
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

            ValidateStock(price, stock);

            ValidateImage(image);
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

        private void ValidateStock(decimal price, int stock)
        {
            DomainExceptionValidation.When(stock < 0, "Invalid stock value, need to be largest to 0!");
            Price = price;
        }

        private void ValidateImage(string image)
        {
            DomainExceptionValidation.When(image?.Length > 250, "Invalid image name, too long, maximum 250 characters!");
            Image = image;
        }
    }
}
