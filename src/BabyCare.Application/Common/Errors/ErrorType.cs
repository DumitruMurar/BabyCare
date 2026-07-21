namespace BabyCare.Application.Common.Errors;

public enum ErrorType
{
    None = 0,

    Validation,

    NotFound,

    Conflict,

    Unauthorized,

    Forbidden,

    Failure,

    Unexpected
}
