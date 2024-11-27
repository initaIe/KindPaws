using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.VolunteerRequests.Domain.ValueObjectsManagement.ValueObjects;

public record CreationTimestamp
{
    private CreationTimestamp(DateTime value)
    {
        Value = value;
    }

    public DateTime Value { get; }

    public static CreationTimestamp CreateNew()
        => new CreationTimestamp(DateTime.UtcNow);

    public static Result<CreationTimestamp, Error> Create(DateTime input)
    {
        if (input > DateTime.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(CreationTimestamp));
        
        return new CreationTimestamp(DateTime.UtcNow);
    }
}