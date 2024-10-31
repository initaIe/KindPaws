using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.HardDelete;

public class HardDeleteBreedCommandValidator : AbstractValidator<HardDeleteBreedCommand>
{
    public HardDeleteBreedCommandValidator()
    {
        RuleFor(d => d.SpecieId)
            .MustBeValueObject(SpecieId.Create);

        RuleFor(d => d.BreedId)
            .MustBeValueObject(BreedId.Create);
    }
}