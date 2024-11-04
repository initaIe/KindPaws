using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateAdditionalInfo;

public class UpdateVolunteerAdditionalInfoCommandValidator : AbstractValidator<UpdateVolunteerAdditionalInfoCommand>
{
    public UpdateVolunteerAdditionalInfoCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);

        RuleFor(u => u.Description)
            .MustBeValueObject(MediumDescription.Create!)
            .When(u => u.Description != null);

        RuleFor(u => u.Address)
            .MustBeValueObject(a => Address.Create(a!.City, a!.Street))
            .When(u => u.Address != null);

        RuleFor(u => u.YearsOfExperience)
            .MustBeValueObject(y => YearsOfExperience.Create(y!.Value))
            .When(u => u.YearsOfExperience != null);

        RuleForEach(u => u.SocialNetworks)
            .MustBeValueObject(s => SocialNetwork.Create(s.Name, s.Link))
            .When(u => u.SocialNetworks != null && u.SocialNetworks.Any());

        RuleForEach(u => u.Requisites)
            .MustBeValueObject(r => Requisite.Create(r.Name, r.Description))
            .When(u => u.Requisites != null && u.Requisites.Any());
    }
}