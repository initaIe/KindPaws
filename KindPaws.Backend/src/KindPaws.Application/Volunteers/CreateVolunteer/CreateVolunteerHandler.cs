using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, Error>> HandleAsync(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var emailAddressResult = EmailAddress.Create(
            request.EmailAddress);

        if (emailAddressResult.IsFailure)
            return emailAddressResult.Error;
        
        // check exist volunteer with Email Address
        var existVolunteerWithEmailAddress = 
            await _volunteersRepository.GetByEmailAddressAsync(emailAddressResult.Value, cancellationToken);

        if (existVolunteerWithEmailAddress.IsSuccess)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer));
        
        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);

        if (phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;
        
        // check exist volunteer with Phone Number
        var existVolunteerWithPhoneNumber = 
            await _volunteersRepository.GetByPhoneNumberAsync(phoneNumberResult.Value, cancellationToken);

        if (existVolunteerWithPhoneNumber.IsSuccess)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer));
        
        var volunteerId = VolunteerId.CreateRandom();

        var fullNameResult = FullName.Create(
            request.FirstName,
            request.LastName,
            request.Patronymic);

        if (fullNameResult.IsFailure)
            return fullNameResult.Error;

        var volunteerToCreate = new Volunteer(
            volunteerId,
            fullNameResult.Value,
            emailAddressResult.Value,
            null,
            null,
            null,
            phoneNumberResult.Value,
            null,
            null);

        await _volunteersRepository.AddAsync(volunteerToCreate, cancellationToken);

        return (Guid)volunteerToCreate.Id;
    }
}