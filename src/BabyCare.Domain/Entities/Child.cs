using BabyCare.Domain.Enums;

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
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;
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
    }
}
