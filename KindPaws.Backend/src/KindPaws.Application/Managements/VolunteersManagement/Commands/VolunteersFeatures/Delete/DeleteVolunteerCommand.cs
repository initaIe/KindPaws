using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId)
    : ICommand
{
    public DeleteVolunteerExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId);
}