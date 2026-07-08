using BabyCare.Domain.Entities;
using BabyCare.Domain.Enums;
using BabyCare.Domain.Exceptions;
using BabyCare.Domain.ValueObjects;
using Xunit;

namespace BabyCare.UnitTests.DomainTests
{
    public class WeightMeasurementTests
    {
        [Fact]
        public void Create_WithNullWeight_ShouldThrowDomainException()
        {
            var action = () => WeightMeasurement.Create(
                null!,
                new DateOnly(2026, 1, 10),
                null);

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Weight is required.", exception.Message);
        }

        [Fact]
        public void Create_WithDefaultMeasuredAt_ShouldThrowDomainException()
        {
            var weight = Weight.Create(10.2m, WeightUnit.Kg);

            var action = () => WeightMeasurement.Create(
                weight,
                default,
                null);

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Measured date is required.", exception.Message);
        }

        [Fact]
        public void Create_WithFutureMeasuredAt_ShouldThrowDomainException()
        {
            var weight = Weight.Create(10.2m, WeightUnit.Kg);
            var futureMeasuredAt = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));

            var action = () => WeightMeasurement.Create(
                weight,
                futureMeasuredAt,
                null);

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Measured date cannot be in the future.", exception.Message);
        }

        [Fact]
        public void Create_WithWhitespaceNotes_ShouldSetNotesToNull()
        {
            var weight = Weight.Create(10.2m, WeightUnit.Kg);

            var weightMeasurement = WeightMeasurement.Create(
                weight,
                new DateOnly(2026, 1, 10),
                "   ");

            Assert.Null(weightMeasurement.Notes);
        }

        [Fact]
        public void Create_WithNotesLongerThanFiveHundredCharacters_ShouldThrowDomainException()
        {
            var weight = Weight.Create(10.2m, WeightUnit.Kg);
            var notes = new string('a', 501);

            var action = () => WeightMeasurement.Create(
                weight,
                new DateOnly(2026, 1, 10),
                notes);

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Notes cannot exceed 500 characters.", exception.Message);
        }
    }
}
