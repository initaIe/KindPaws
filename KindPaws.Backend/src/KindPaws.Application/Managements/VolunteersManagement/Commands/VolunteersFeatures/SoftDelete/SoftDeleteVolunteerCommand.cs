using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.SoftDelete;

public record SoftDeleteVolunteerCommand(Guid VolunteerId)
    : ICommand
{
    public SoftDeleteVolunteerExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId);
}