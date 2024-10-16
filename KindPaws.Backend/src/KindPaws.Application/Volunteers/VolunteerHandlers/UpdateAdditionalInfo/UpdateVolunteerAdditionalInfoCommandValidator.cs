using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.VolunteerHandlers.UpdateAdditionalInfo;

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