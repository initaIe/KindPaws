using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Delete;

public record DeleteVolunteerExistenceValidationData(
    Guid VolunteerId)
    : IExistenceValidationData;