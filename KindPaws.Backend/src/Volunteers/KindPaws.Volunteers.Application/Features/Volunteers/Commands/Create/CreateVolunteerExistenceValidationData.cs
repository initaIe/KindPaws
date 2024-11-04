using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.Create;

public record CreateVolunteerExistenceValidationData(
    string EmailAddress,
    string PhoneNumber)
    : IExistenceValidationData;