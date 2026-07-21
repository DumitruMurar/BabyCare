namespace BabyCare.Application.Common.Errors.Catalog;

public static class CommonErrors
{
    public static readonly Error Unexpected =
        new(
            "Common.Unexpected",
            "An unexpected error occurred.",
            ErrorType.Unexpected);

    public static readonly Error Validation =
        new(
            "Common.Validation",
            "One or more validation errors occurred.",
            ErrorType.Validation);
}