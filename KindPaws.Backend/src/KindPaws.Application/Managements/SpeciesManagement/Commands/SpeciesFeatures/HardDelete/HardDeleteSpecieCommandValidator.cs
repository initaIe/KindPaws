using FluentValidation;
using KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.SoftDelete;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.HardDelete;

public class HardDeleteSpecieCommandValidator : AbstractValidator<HardDeleteSpecieCommand>
{
    public HardDeleteSpecieCommandValidator()
    {
        RuleFor(d => d.SpecieId)
            .MustBeValueObject(SpecieId.Create);
    }
}