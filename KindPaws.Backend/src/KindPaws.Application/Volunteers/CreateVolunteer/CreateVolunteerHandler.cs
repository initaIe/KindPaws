using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

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
        var emailAddress = EmailAddress.Create(request.EmailAddress).Value;

        // check email is already exist
        var existVolunteerWithEmailAddress =
            await _volunteersRepository.GetByEmailAddressAsync(emailAddress, cancellationToken);

        if (existVolunteerWithEmailAddress.IsSuccess)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer));

        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;

        // check phone number already exist
        var existVolunteerWithPhoneNumber =
            await _volunteersRepository.GetByPhoneNumberAsync(phoneNumber, cancellationToken);

        if (existVolunteerWithPhoneNumber.IsSuccess)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer));

        var volunteerId = VolunteerId.CreateRandom();

        var fullName = FullName.Create(
            request.FirstName,
            request.LastName,
            request.Patronymic).Value;

        var volunteerToCreate = new Volunteer(
            volunteerId,
            fullName,
            emailAddress,
            null,
            null,
            null,
            phoneNumber,
            null,
            null);

        await _volunteersRepository.AddAsync(volunteerToCreate, cancellationToken);

        return (Guid)volunteerToCreate.Id;
    }
}