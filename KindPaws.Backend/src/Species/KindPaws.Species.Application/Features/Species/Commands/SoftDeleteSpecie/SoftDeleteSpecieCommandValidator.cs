using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Species.Application.Features.Species.Commands.SoftDeleteSpecie;

public class SoftDeleteSpecieCommandValidator : AbstractValidator<SoftDeleteSpecieCommand>
{
    public SoftDeleteSpecieCommandValidator()
    {
        RuleFor(d => d.SpecieId)
            .MustBeValueObject(SpecieId.Create);
    }
}