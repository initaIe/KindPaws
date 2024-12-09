using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record PermissionId
{
    private PermissionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PermissionId CreateRandom()
    {
        return new PermissionId(Guid.NewGuid());
    }

    public static Result<PermissionId, Error> Create(Guid input)
    {
        if (GuidValidator.IsEmpty(input))
            return ErrorsGeneral.ValueIsInvalid();

        return new PermissionId(input);
    }
}