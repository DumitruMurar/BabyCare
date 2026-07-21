using BabyCare.Domain.Enums;
using BabyCare.Domain.Exceptions;

namespace BabyCare.Domain.ValueObjects
{
    public sealed class Weight
    {
        public decimal Value { get; }

        public WeightUnit Unit { get; }

        private Weight(
            decimal value,
            WeightUnit unit
            )
        {
            Value = ValidateValue(value);
            Unit = ValidateUnit(unit);
        }

        public static Weight Create(decimal value, WeightUnit unit)
        {
            return new Weight(value, unit);
        }

        private static decimal ValidateValue(decimal value)
        {
            if (value <= 0)
            {
                throw new DomainException("Value must be greater than zero.");
            }

            return value;
        }

        private static WeightUnit ValidateUnit(WeightUnit unit)
        {

            if (!Enum.IsDefined(unit))
            {
                throw new DomainException($"Invalid weight unit");
            }

            return unit;
        }
    }
}