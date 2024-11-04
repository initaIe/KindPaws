using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateMainInfo;

public record UpdateVolunteerMainInfoExistenceValidationData(
    Guid VolunteerId,
    string EmailAddress,
    string PhoneNumber)
    : IExistenceValidationData;