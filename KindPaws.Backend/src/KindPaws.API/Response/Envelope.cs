namespace KindPaws.API.Response;

public record Envelope
{
    private Envelope(object? result, IEnumerable<ResponseError> errors)
    {
        Result = result;
        Errors = errors.ToList();
        CreationDateTime = DateTime.Now;
    }

    public object? Result { get; }

    public List<ResponseError> Errors { get; } = [];
    public DateTime CreationDateTime { get; }

    public static Envelope Ok(object? result = null)
    {
        return new Envelope(result, []);
    }

    public static Envelope Error(IEnumerable<ResponseError> errors)
    {
        return new Envelope(null, errors.ToList());
    }
}