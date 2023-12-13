namespace E_CommerceApp.Common.Infrastructure.Exceptions;

public class DatabaseValidationException : Exception
{
    public DatabaseValidationException()
    {

    }
    public DatabaseValidationException(string? message) : base(message)
    {

    }
}