using KindPaws.Domain.Shared.Others;

namespace KindPaws.API.Response;

public record Envelope
{
    private Envelope(object? result, ErrorList? errors)
    {
        Result = result;
        Errors = errors;
        CreationDateTime = DateTime.Now;
    }

    public object? Result { get; }
    public ErrorList? Errors { get; } 
    public DateTime CreationDateTime { get; }

    public static Envelope Ok(object? result = null)
    {
        return new Envelope(result, null);
    }

    public static Envelope Error(ErrorList errors)
    {
        return new Envelope(null, errors);
    }
}