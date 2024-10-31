using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.SoftDelete;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.HardDelete;

public record HardDeleteVolunteerCommand(Guid VolunteerId)
    : ICommand
{
    public HardDeleteVolunteerExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId);
}