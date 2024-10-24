using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

namespace KindPaws.Application.Managements.SpeciesManagment.Commands.SpeciesFeatures.Create;

public class CreateSpecieCommandValidator : AbstractValidator<CreateSpecieCommand>
{
    public CreateSpecieCommandValidator()
    {
        RuleFor(c => c.Name)
            .MustBeValueObject(ShortName.Create);

        RuleFor(c => c.Description)
            .MustBeValueObject(MediumDescription.Create);
    }
}