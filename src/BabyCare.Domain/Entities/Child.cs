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

        public Child()
        {

        }

        public static Child Create()
        {
            return new Child();
        }
    }
}
