using BabyCare.Domain.Enums;
using BabyCare.Domain.Exceptions;
using BabyCare.Domain.ValueObjects;
using Xunit;

namespace BabyCare.UnitTests.DomainTests
{
    public class WeightTests
    {
        [Fact]
        public void Create_WithValidWeight_ShouldCreateWeight()
        {
            var weight = Weight.Create(
                10.2m,
                WeightUnit.Kg
                );

            Assert.Equal(10.2m, weight.Value);
            Assert.Equal(WeightUnit.Kg, weight.Unit);
        }

        [Fact]
        public void Create_WithZeroWeight_ShouldThrowDomainException()
        {
            var action = () => Weight.Create(
                0,
                WeightUnit.Kg
                );

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Value must be greater than zero.", exception.Message);
        }

        [Fact]
        public void Create_WithNegativeWeight_ShouldThrowDomainException()
        {
            var action = () => Weight.Create(
                -10,
                WeightUnit.Kg
                );

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Value must be greater than zero.", exception.Message);
        }

        [Fact]
        public void Create_WithInvalidWeightUnit_ShouldThrowDomainException()
        {
            var action = () => Weight.Create(
                10.2m,
                (WeightUnit)999
                );

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Invalid weight unit", exception.Message);
        }
    }
}
