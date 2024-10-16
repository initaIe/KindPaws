using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.PetHandlers.UpdateAdditionalInfo;

public class UpdatePetAdditionalInfoCommandValidator : AbstractValidator<UpdatePetAdditionalInfoCommand>
{
    public UpdatePetAdditionalInfoCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);
        
        RuleFor(u => u.PetId)
            .MustBeValueObject(PetId.Create);   
        
        RuleFor(u => u.SupportStatus)
            .MustBeValueObject(SupportStatus.Create!)
            .When(u=>u.SupportStatus != null); 
        
        RuleFor(u => u.Description)
            .MustBeValueObject(MediumDescription.Create!)
            .When(u=>u.Description != null); 
        
        RuleFor(u => u.PetColor)
            .MustBeValueObject(PetColor.Create!)
            .When(u=>u.PetColor != null); 
        
        RuleFor(u => u.BirthDate)
            .MustBeValueObject(d=>Age.Create(d!.Value))
            .When(u=>u.BirthDate != null);

        RuleFor(u => u.HealthDetails!.Description)
            .MustBeValueObject(MediumDescription.Create!)
            .When(u => u.HealthDetails is { Description: not null });

        RuleForEach(u => u.HealthDetails!.Vaccines)
            .MustBeValueObject(Vaccine.Create)
            .When(u => u.HealthDetails != null && u.HealthDetails.Vaccines!.Any());
        
        RuleForEach(u => u.HealthDetails!.Diseases)
            .MustBeValueObject(Disease.Create)
            .When(u => u.HealthDetails != null && u.HealthDetails.Diseases!.Any());
        
        RuleFor(u => u.HealthDetails!.HealthStatus)
            .MustBeValueObject(HealthStatus.Create!)
            .When(u => u.HealthDetails is { HealthStatus: not null });
        
        RuleFor(u => u.BiometricDetails!.Height)
            .MustBeValueObject(h=>Height.Create(h!.Value))
            .When(u => u.BiometricDetails is { Height: not null });
        
        RuleFor(u => u.BiometricDetails!.Weight)
            .MustBeValueObject(w=>Weight.Create(w!.Value))
            .When(u => u.BiometricDetails is { Weight: not null });
        
        RuleFor(u => u.BiometricDetails!.Gender)
            .MustBeValueObject(Gender.Create!)
            .When(u => u.BiometricDetails is { Gender: not null });
    }
}