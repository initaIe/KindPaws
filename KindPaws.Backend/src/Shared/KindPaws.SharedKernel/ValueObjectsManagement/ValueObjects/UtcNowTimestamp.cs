using System.Runtime.InteropServices.JavaScript;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record UtcNowTimestamp
{
    private UtcNowTimestamp(DateTime dateTime)
    {
        Value = dateTime;
    }

    public DateTime Value { get; }

    public static UtcNowTimestamp CreateNew()
    {
        return new UtcNowTimestamp(DateTime.UtcNow);
    }
    
    public static UtcNowTimestamp Create(DateTime dateTime)
    {
        return new UtcNowTimestamp(dateTime);
    }
}