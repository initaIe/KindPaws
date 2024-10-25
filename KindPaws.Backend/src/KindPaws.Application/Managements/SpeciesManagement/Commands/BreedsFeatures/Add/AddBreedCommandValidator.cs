using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;

public class AddBreedCommandValidator : AbstractValidator<AddBreedCommand>
{
    public AddBreedCommandValidator()
    {
        RuleFor(a => a.SpecieId)
            .MustBeValueObject(SpecieId.Create);

        RuleFor(a => a.Name)
            .MustBeValueObject(ShortName.Create);

        RuleFor(a => a.Description)
            .MustBeValueObject(MediumDescription.Create);
    }
}