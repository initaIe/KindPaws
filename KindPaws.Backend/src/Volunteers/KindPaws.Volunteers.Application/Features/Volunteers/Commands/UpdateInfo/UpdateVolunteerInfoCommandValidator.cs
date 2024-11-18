using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateInfo;

public class UpdateVolunteerInfoCommandValidator : AbstractValidator<UpdateVolunteerInfoCommand>
{
    public UpdateVolunteerInfoCommandValidator()
    {
        RuleFor(c => c.Description)
            .MustBeValueObject(MediumString.Create!)
            .When(c => c.Description != null);
        
        RuleFor(c => c.Address)
            .MustBeValueObject(a=>Address.Create(a!.City, a!.Street))
            .When(c => c.Address != null);
        
        RuleFor(c => c.YearsOfExperience)
            .MustBeValueObject(y=>YearsOfExperience.Create(y!.Value))
            .When(c => c.YearsOfExperience != null);

        RuleForEach(c => c.Requisites)
            .MustBeValueObject(r => Requisite.Create(r.Name, r.Description));
    }
}