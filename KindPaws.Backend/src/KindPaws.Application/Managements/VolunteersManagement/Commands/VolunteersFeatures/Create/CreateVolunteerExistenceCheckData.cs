using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;

public record CreateVolunteerExistenceCheckData(
    string EmailAddress,
    string PhoneNumber)
    : IExistenceCheckData;