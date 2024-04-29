
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : EntityBase
    {
        public string Name { get; private set; }

        public ICollection<Product> Products { get; private set; }

        public Category(string name)
        {
            ValidateDomain(name);
        }

        public Category(int id, string name)
        {
            ValidadeId(id);
            ValidateDomain(name);
        }

        public void Update(string name)
        {
            ValidateDomain(name);
        }

        private void ValidateDomain(string name)
        {
            ValidateName(name);
        }

        private void ValidadeId(int id)
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
    }
}
