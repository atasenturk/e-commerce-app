namespace E_CommerceApp.Common.Infrastructure.Exceptions;

public class DatabaseDuplicateException : Exception
{
    public DatabaseDuplicateException()
    {

    }
    public DatabaseDuplicateException(string? message) : base(message)
    {

    }
}