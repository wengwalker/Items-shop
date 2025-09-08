namespace Catalog.Domain.Exceptions;

public class NotEnoughProductQuantityException : Exception
{
    public NotEnoughProductQuantityException() { }

    public NotEnoughProductQuantityException(string? message) : base(message) { }

    public NotEnoughProductQuantityException(string? message, Exception? innerException) : base(message, innerException) { }
}
