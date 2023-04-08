namespace PicPig.Exceptions;

public class ServiceException : Exception
{
    public readonly IReadOnlyDictionary<string, string>? Details;

    public ServiceException(string message, IReadOnlyDictionary<string, string>? details = null) : base(message)
    {
        Details = details;
    }

    public ServiceException(string message, Exception innerException, IReadOnlyDictionary<string, string>? details = null) : base(message, innerException)
    {
        Details = details;
    }
}
