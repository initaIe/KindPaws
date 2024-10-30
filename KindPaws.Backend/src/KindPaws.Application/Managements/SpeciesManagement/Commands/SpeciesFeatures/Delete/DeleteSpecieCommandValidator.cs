using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Delete;

public class DeleteSpecieCommandValidator : AbstractValidator<DeleteSpecieCommand>
{
    public DeleteSpecieCommandValidator()
    {
        RuleFor(d => d.SpecieId)
            .MustBeValueObject(SpecieId.Create);
    }
}