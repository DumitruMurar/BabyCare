using BabyCare.Domain.Entities;
using BabyCare.Domain.Enums;
using BabyCare.Domain.Exceptions;
using BabyCare.Domain.ValueObjects;
using Xunit;

namespace BabyCare.UnitTests.DomainTests
{

    public class ChildTests
    {
        [Fact]
        public void Create_WithValidData_ShouldCreateChild()
        {
            // Arrange
            var parentId = Guid.NewGuid();

            // Act
            var child = Child.Create(
                parentId,
                "Matei",
                "Popescu",
                new DateOnly(2024, 1, 10),
                Gender.Boy);

            // Assert
            Assert.NotNull(child);

            Assert.Equal(parentId, child.ParentId);
            Assert.Equal("Matei", child.FirstName);
            Assert.Equal("Popescu", child.LastName);
            Assert.Equal(new DateOnly(2024, 1, 10), child.BirthDate);
            Assert.Equal(Gender.Boy, child.Gender);
            Assert.NotEqual(child.Id, Guid.Empty);
        }

        [Fact]
        public void Create_WithEmptyFirstName_ShouldThrowDomainException()
        {
            // Arrange
            var parentId = Guid.NewGuid();

            // Act
            var action = () => Child.Create(
                parentId,
                "",
                "Popescu",
                new DateOnly(2024, 1, 10),
                Gender.Boy);

            // Assert
            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("FirstName is required.", exception.Message);
        }

        [Fact]
        public void Create_WithEmptyLastName_ShouldThrowDomainException()
        {
            // Arrange
            var parentId = Guid.NewGuid();

            // Act
            var action = () => Child.Create(
                parentId,
                "Matei",
                "",
                new DateOnly(2024, 1, 10),
                Gender.Boy);

            // Assert
            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("LastName is required.", exception.Message);
        }

        [Fact]
        public void Create_WithEmptyParentId_ShouldThrowDomainException()
        {
            // Act
            var action = () => Child.Create(
                Guid.Empty,
                "Matei",
                "Popescu",
                new DateOnly(2024, 1, 10),
                Gender.Boy);

            // Assert
            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("ParentId should not be empty", exception.Message);
        }

        [Fact]
        public void Create_BirthDateInFuture_ShouldThrowDomainException()
        {
            // Arrange
            var parentId = Guid.NewGuid();

            // Act
            var action = () => Child.Create(
                parentId,
                "Matei",
                "Popescu",
                new DateOnly(2027, 1, 10),
                Gender.Boy);

            // Assert
            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Birth date cannot be in the future.", exception.Message);
        }

        [Fact]
        public void Create_WithSpacesAroundFirstName_ShouldTrimName()
        {
            // Arrange
            var parentId = Guid.NewGuid();

            // Act
            var child = Child.Create(
                parentId,
                "Matei  ",
                "Popescu",
                new DateOnly(2024, 1, 10),
                Gender.Boy);

            // Assert
            Assert.Equal("Matei", child.FirstName);
        }

        [Fact]
        public void AddWeight_WithFirstMeasurement_ShouldAddWeightMeasurement()
        {
            // Arrange
            var child = Child.Create(
                Guid.NewGuid(),
                "Matei",
                "Popescu",
                new DateOnly(2024, 1, 10),
                Gender.Boy);
            var weight = Weight.Create(10.2m, WeightUnit.Kg);
            var measuredAt = new DateOnly(2026, 1, 10);

            // Act
            var weightMeasurement = child.AddWeight(
                weight,
                measuredAt,
                "Morning measurement");

            // Assert
            Assert.NotNull(weightMeasurement);
            Assert.NotEqual(Guid.Empty, weightMeasurement.Id);
            Assert.Equal(weight, weightMeasurement.Weight);
            Assert.Equal(measuredAt, weightMeasurement.MeasuredAt);
            Assert.Equal("Morning measurement", weightMeasurement.Notes);
            Assert.Single(child.WeightMeasurements);
        }

        [Fact]
        public void AddWeight_WithExistingMeasuredAt_ShouldThrowDomainException()
        {
            // Arrange
            var child = Child.Create(
                Guid.NewGuid(),
                "Matei",
                "Popescu",
                new DateOnly(2024, 1, 10),
                Gender.Boy);
            var firstWeight = Weight.Create(10.2m, WeightUnit.Kg);
            var secondWeight = Weight.Create(10.4m, WeightUnit.Kg);
            var measuredAt = new DateOnly(2026, 1, 10);

            child.AddWeight(
                firstWeight,
                measuredAt,
                null);

            // Act
            var action = () => child.AddWeight(
                secondWeight,
                measuredAt,
                null);

            // Assert
            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Weight measurement already exists for this date.", exception.Message);
        }
    }
}
