using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Core.Models;

public record Envelope
{
    private Envelope(object? result, ErrorList? errors)
    {
        Result = result;
        Errors = ResponseErrorList.FromErrorList(errors);
        CreationDateTime = DateTime.Now;
    }

    public object? Result { get; }
    public ResponseErrorList? Errors { get; }
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