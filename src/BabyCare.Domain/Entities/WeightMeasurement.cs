using BabyCare.Domain.Exceptions;
using BabyCare.Domain.ValueObjects;

namespace BabyCare.Domain.Entities
{
    public sealed class WeightMeasurement
    {
        public Guid Id { get; }

        public Weight Weight { get; }

        public DateOnly MeasuredAt { get; }

        public string? Notes { get; }

        private WeightMeasurement(
            Guid id,
            Weight weight,
            DateOnly measuredAt,
            string? notes
            )
        {
            Id = id;
            Weight = ValidateWeight(weight);
            MeasuredAt = ValidateMeasuredAt(measuredAt);
            Notes = ValidateNotes(notes);
        }

        public static WeightMeasurement Create(
            Weight weight,
            DateOnly measuredAt,
            string? notes)
        {
            return new WeightMeasurement(
                Guid.NewGuid(),
                weight,
                measuredAt,
                notes);
        }

        private static Weight ValidateWeight(Weight weight)
        {
            if (weight is null)
            {
                throw new DomainException("Weight is required.");
            }

            return weight;
        }

        private static DateOnly ValidateMeasuredAt(DateOnly measuredAt)
        {
            if (measuredAt == default)
            {
                throw new DomainException("Measured date is required.");
            }

            if (measuredAt > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new DomainException("Measured date cannot be in the future.");
            }

            return measuredAt;
        }

        private static string? ValidateNotes(string? notes)
        {
            if (string.IsNullOrWhiteSpace(notes))
                return null;

            var trimmedNotes = notes.Trim();

            if (trimmedNotes.Length > 500)
            {
                throw new DomainException("Notes cannot exceed 500 characters.");
            }

            return trimmedNotes;
        }
    }
}
