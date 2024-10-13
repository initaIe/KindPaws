using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Application.Volunteers.UpdateAdditionalInfo.DTOs;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

namespace KindPaws.Application.Volunteers.UpdateAdditionalInfo.Validators;

public class UpdateVolunteerAdditionalInfoDTOValidator : AbstractValidator<UpdateVolunteerAdditionalInfoDTO>
{
    public UpdateVolunteerAdditionalInfoDTOValidator()
    {
        RuleFor(u => u.Description)
            .MustBeValueObject(MediumDescription.Create!)
            .When(u => u.Description != null);

        RuleFor(u => u.Address)
            .MustBeValueObject(a => Address.Create(a!.City, a!.Street))
            .When(u => u.Address != null);

        RuleFor(u => u.YearsOfExperience)
            .MustBeValueObject(x => YearsOfExperience.Create(x!.Value))
            .When(u => u.YearsOfExperience != null);

        RuleForEach(u => u.SocialNetworks)
            .MustBeValueObject(s => SocialNetwork.Create(s.Name, s.Link))
            .When(u => u.SocialNetworks != null && u.SocialNetworks.Any());

        RuleForEach(u => u.Requisites)
            .MustBeValueObject(s => Requisite.Create(s.Name, s.Description))
            .When(u => u.Requisites != null && u.Requisites.Any());
    }
}