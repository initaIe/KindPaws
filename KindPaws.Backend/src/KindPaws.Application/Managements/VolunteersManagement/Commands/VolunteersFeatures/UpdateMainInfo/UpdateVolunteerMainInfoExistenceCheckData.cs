using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateMainInfo;

public record UpdateVolunteerMainInfoExistenceCheckData(
    Guid VolunteerId,
    string EmailAddress,
    string PhoneNumber)
    : IExistenceCheckData;