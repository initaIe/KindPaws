using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Species.Application.Features.Species.Commands.Create;

public class CreateSpecieCommandValidator : AbstractValidator<CreateSpecieCommand>
{
    public CreateSpecieCommandValidator()
    {
        RuleFor(c => c.Name)
            .MustBeValueObject(SpecieName.Create);

        RuleFor(c => c.Description)
            .MustBeValueObject(SpecieDescription.Create);
    }
}