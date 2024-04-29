using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using Xunit.Sdk;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact(DisplayName = "Create Product with valid state")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99M, 99,
                                                "Product Image");
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product with invalid Id")]
        public void CreateProduct_WithNegativeIdValue_ResultDomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99M, 99,
                                                "Product Image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid Id value, need to be largest to 0!");
        }

        [Theory(DisplayName = "Create Product with Name shorter than 3 characters")]
        [InlineData("A")]
        [InlineData("AB")]
        public void CreateProduct_WithNameShorterThan3Characters_ResultDomainExceptionShortName(string name)
        {
            Action action = () => new Product(1, name, "Product Description", 9.99M, 99,
                                                "Product Image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name, too short, minimum is 3 characters!");
        }

        [Theory(DisplayName = "Create Product with Name equals null or empty")]
        [InlineData(null)]
        [InlineData("")]
        public void CreateProduct_WithNameEqualsNullOrEmpty_ResultDomainExceptionRequiredName(string name)
        {
            Action action = () => new Product(1, name, "Product Description", 9.99M, 99,
                                                "Product Image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid, name is required!");
        }

        [Theory(DisplayName = "Create Product with Description shorter than 5 characters")]
        [InlineData("A")]
        [InlineData("AB")]
        [InlineData("ABC")]
        [InlineData("ABCD")]
        public void CreateProduct_WithDescriptionShorterThan5Characters_ResultDomainExceptionShortDescription(string description)
        {
            Action action = () => new Product(1, "Product Name", description, 9.99M, 99,
                                                "Product Image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid description, too short, minimum is 5 characters!");
        }

        [Theory(DisplayName = "Create Product with Description equals null or empty")]
        [InlineData(null)]
        [InlineData("")]
        public void CreateProduct_WithDescriptionEqualsNullOrEmpty_ResultDomainExceptionRequiredDescription(string description)
        {
            Action action = () => new Product(1, "Product Name", description, 9.99M, 99,
                                                "Product Image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid, description is required!");
        }

        [Fact(DisplayName = "Create Product with invalid Price")]
        public void CreateProduct_WithNegativePriceValue_ResultDomainExceptionInvalidPrice()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -0.01M, 99,
                                                "Product Image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid price value, need to be largest to 0!");
        }

        [Fact(DisplayName = "Create Product with invalid Stock")]
        public void CreateProduct_WithNegativeStockValue_ResultDomainExceptionInvalidStock()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99M, -1,
                                                "Product Image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid stock value, need to be largest to 0!");
        }

        [Fact(DisplayName = "Create Product with Image longer than 250 characters")]
        public void CreateProduct_WithImageLongerThan5Characters_ResultDomainExceptionLongImage()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99M, 99,
                                                new string('A', 251));
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid image name, too long, maximum 250 characters!");
        }

        [Theory(DisplayName = "Create Product with Image equals null or empty")]
        [InlineData(null)]
        [InlineData("")]
        public void CreateProduct_WithImageEqualsNullOrEmpty_ResultNoDomainException(string image)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99M, 99,
                                                image);
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product with Image equals null")]
        public void CreateProduct_WithImageEqualsNull_ResultNoNullReferenceException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99M, 99, null);
            action.Should().NotThrow<NullReferenceException>();
        }
    }
}
