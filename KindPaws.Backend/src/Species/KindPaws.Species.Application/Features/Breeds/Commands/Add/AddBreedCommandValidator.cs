using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Species.Application.Features.Breeds.Commands.Add;

public class AddBreedCommandValidator : AbstractValidator<AddBreedCommand>
{
    public AddBreedCommandValidator()
    {
        RuleFor(a => a.SpecieId)
            .MustBeValueObject(SpecieId.Create);

        RuleFor(a => a.Name)
            .MustBeValueObject(ShortAlphabeticString.Create);

        RuleFor(a => a.Description)
            .MustBeValueObject(MediumString.Create);
    }
}