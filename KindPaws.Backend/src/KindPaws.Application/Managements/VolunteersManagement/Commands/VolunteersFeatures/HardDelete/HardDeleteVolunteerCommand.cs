using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.HardDelete;

public record HardDeleteVolunteerCommand(Guid VolunteerId)
    : ICommand
{
    public HardDeleteVolunteerExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId);
}