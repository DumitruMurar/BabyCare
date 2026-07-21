namespace BabyCare.Application.Common.Errors.Catalog;

public static class ChildErrors
{
    public static readonly Error NotFound =
        new(
            "Children.NotFound",
            "The requested child was not found.",
            ErrorType.NotFound);

    public static readonly Error ParentNotFound =
        new(
            "Children.ParentNotFound",
            "The specified parent does not exist.",
            ErrorType.NotFound);

    public static readonly Error AlreadyExists =
        new(
            "Children.AlreadyExists",
            "The child already exists.",
            ErrorType.Conflict);
}