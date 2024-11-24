using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Species.Application.Features.Breeds.Commands.SoftDeleteBreed;

public class SoftDeleteBreedCommandValidator : AbstractValidator<SoftDeleteBreedCommand>
{
    public SoftDeleteBreedCommandValidator()
    {
        RuleFor(d => d.SpecieId)
            .MustBeValueObject(SpecieId.Create);

        RuleFor(d => d.BreedId)
            .MustBeValueObject(BreedId.Create);
    }
}