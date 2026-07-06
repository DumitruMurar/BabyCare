using BabyCare.Domain.Enums;
using BabyCare.Domain.Exceptions;

namespace BabyCare.Domain.Entities
{
    public sealed class Child
    {
        public Guid Id { get; }

        public Guid ParentId { get; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateOnly BirthDate { get; private set; }

        public Gender Gender { get; private set; }

        private Child(
            Guid id,
            Guid parentId,
            string firstName,
            string lastName,
            DateOnly birthDate,
            Gender gender)
        {
            Id = id;
            ParentId = parentId;
            FirstName = ValidateName(firstName, nameof(FirstName));
            LastName = ValidateName(lastName, nameof(LastName));
            BirthDate = ValidateBirthDate(birthDate);
            Gender = ValidateGender(gender);
        }

        public static Child Create(
            Guid parentId,
            string firstName,
            string lastName,
            DateOnly birthDate,
            Gender gender
            )
        {
            return new Child(
                Guid.NewGuid(),
                parentId,
                firstName,
                lastName,
                birthDate,
                gender
                );
        }

        private static string ValidateName(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException($"{fieldName} is required.");
            }

            var trimmedValue = value.Trim();

            if (trimmedValue.Length > 100)
            {
                throw new DomainException($"{fieldName} cannot exceed 100 characters.");
            }

            if (!trimmedValue.All(c => char.IsLetter(c) || c == ' ' || c == '-' || c == '\''))
            {
                throw new DomainException($"{fieldName} contains invalid characters.");
            }

            return trimmedValue;
        }

        private static DateOnly ValidateBirthDate(DateOnly birthDate)
        {
            if (birthDate == default)
            {
                throw new DomainException("Birth date is required.");
            }

            if (birthDate > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new DomainException("Birth date cannot be in the future.");
            }

            return birthDate;
        }

        private static Gender ValidateGender(Gender gender)
        {
            if (!Enum.IsDefined(gender))
            {
                throw new DomainException("Gender is invalid.");
            }

            return gender;
        }
    }
}
