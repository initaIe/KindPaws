using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record DiscussionId
{
    private DiscussionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static DiscussionId CreateRandom()
    {
        return new DiscussionId(Guid.NewGuid());
    }

    public static Result<DiscussionId, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid();

        return new DiscussionId(value);
    }
}