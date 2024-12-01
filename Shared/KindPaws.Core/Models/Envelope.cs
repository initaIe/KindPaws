using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Core.Models;

public record Envelope
{
    private Envelope(object? result, ErrorList? errors)
    {
        Result = result;
        Errors = ResponseErrorList.FromErrorList(errors);
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public object? Result { get; }
    public ResponseErrorList? Errors { get; }
    public DateTimeOffset CreatedAt { get; }

    public static Envelope Ok(object? result = null)
    {
        return new Envelope(result, null);
    }

    public static Envelope Error(ErrorList errors)
    {
        return new Envelope(null, errors);
    }
}