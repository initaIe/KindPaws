using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;

namespace KindPaws.Species.Application.Features.Species.Commands.Create;

public class CreateSpecieCommandValidator : AbstractValidator<CreateSpecieCommand>
{
    public CreateSpecieCommandValidator()
    {
        RuleFor(c => c.Name)
            .MustBeValueObject(ShortAlphabeticString.Create);

        RuleFor(c => c.Description)
            .MustBeValueObject(MediumString.Create);
    }
}