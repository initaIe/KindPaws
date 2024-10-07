using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Shared.ValueObjects.IDs;

public record BreedId
{
    private BreedId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static BreedId CreateRandom()
    {
        return new BreedId(Guid.NewGuid());
    }

    public static BreedId CreateEmpty()
    {
        return new BreedId(Guid.Empty);
    }

    public static Result<BreedId, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid();

        return new BreedId(value);
    }

    public static implicit operator Guid(BreedId breedId)
    {
        return breedId?.Value
               ?? throw new ArgumentNullException($"{nameof(breedId)} cannot be null.");
    }
}