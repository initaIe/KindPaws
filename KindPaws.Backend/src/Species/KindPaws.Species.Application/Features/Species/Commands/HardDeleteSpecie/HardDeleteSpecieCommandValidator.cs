using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Species.Application.Features.Species.Commands.HardDeleteSpecie;

public class HardDeleteSpecieCommandValidator : AbstractValidator<HardDeleteSpecieCommand>
{
    public HardDeleteSpecieCommandValidator()
    {
        RuleFor(d => d.SpecieId)
            .MustBeValueObject(SpecieId.Create);
    }
}