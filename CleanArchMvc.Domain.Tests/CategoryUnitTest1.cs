using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact(DisplayName = "Create Category with valid state")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Category Name");
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category with invalid Id")]
        public void CreateCategory_WithNegativeIdValue_ResultDomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Category Name");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid Id value, need to be largest to 0!");
        }

        [Theory(DisplayName = "Create Category with Name shorter than 3 characters")]
        [InlineData("A")]
        [InlineData("AB")]
        public void CreateCategory_WithNameShorterThan3Characters_ResultDomainExceptionShortName(string name)
        {
            Action action = () => new Category(1, name);
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name, too short, minimum is 3 characters!");
        }

        [Theory(DisplayName = "Create Category with Name equals null or empty")]
        [InlineData(null)]
        [InlineData("")]
        public void CreateCategory_WithNameEqualsNullOrEmpty_ResultDomainExceptionRequiredName(string name)
        {
            Action action = () => new Category(1, name);
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid, name is required!");
        }
    }
}
