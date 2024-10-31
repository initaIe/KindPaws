using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.SoftDelete;

public record SoftDeleteVolunteerExistenceValidationData(
    Guid VolunteerId)
    : IExistenceValidationData;