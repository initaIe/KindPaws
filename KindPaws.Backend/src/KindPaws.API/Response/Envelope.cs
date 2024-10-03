using KindPaws.Domain.Shared.Others;

namespace KindPaws.API.Response;

public record Envelope
{
    private Envelope(object? result, Error? error)
    {
        Result = result;
        ErrorCode = error?.Code;
        ErrorMessage = error?.Message;
        CreationDateTime = DateTime.Now;
    }

    public object? Result { get; }
    public string? ErrorCode { get; }
    public string? ErrorMessage { get; }
    public DateTime CreationDateTime { get; }

    public static Envelope Ok(object? result = null)
    {
        return new Envelope(result, null);
    }
    
    public static Envelope Error(Error error)
    {
        return new Envelope(null, error);
    }
}