using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.HardDelete;

public record HardDeleteVolunteerExistenceValidationData(Guid VolunteerId)
    : IExistenceValidationData;