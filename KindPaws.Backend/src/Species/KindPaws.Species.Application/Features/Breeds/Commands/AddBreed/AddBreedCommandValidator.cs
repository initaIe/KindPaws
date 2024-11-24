using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Species.Application.Features.Breeds.Commands.Add;

public class AddBreedCommandValidator : AbstractValidator<AddBreedCommand>
{
    public AddBreedCommandValidator()
    {
        RuleFor(a => a.SpecieId)
            .MustBeValueObject(SpecieId.Create);

        RuleFor(a => a.Name)
            .MustBeValueObject(BreedName.Create);

        RuleFor(a => a.Description)
            .MustBeValueObject(BreedDescription.Create);
    }
}