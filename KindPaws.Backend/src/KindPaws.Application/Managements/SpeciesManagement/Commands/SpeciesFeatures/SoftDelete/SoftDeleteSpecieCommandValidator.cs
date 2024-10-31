using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.SoftDelete;

public class SoftDeleteSpecieCommandValidator : AbstractValidator<SoftDeleteSpecieCommand>
{
    public SoftDeleteSpecieCommandValidator()
    {
        RuleFor(d => d.SpecieId)
            .MustBeValueObject(SpecieId.Create);
    }
}