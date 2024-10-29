using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;

public record CreateVolunteerExistenceValidationData(
    string EmailAddress,
    string PhoneNumber)
    : IExistenceValidationData;