using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Species.Application.Features.Breeds.Commands.HardDelete;

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