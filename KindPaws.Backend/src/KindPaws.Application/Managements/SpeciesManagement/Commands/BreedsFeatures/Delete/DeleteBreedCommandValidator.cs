using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Delete;

public class DeleteBreedCommandValidator : AbstractValidator<DeleteBreedCommand>
{
    public DeleteBreedCommandValidator()
    {
        RuleFor(d => d.SpecieId)
            .MustBeValueObject(SpecieId.Create);
        
        RuleFor(d => d.BreedId)
            .MustBeValueObject(BreedId.Create);
    }
}