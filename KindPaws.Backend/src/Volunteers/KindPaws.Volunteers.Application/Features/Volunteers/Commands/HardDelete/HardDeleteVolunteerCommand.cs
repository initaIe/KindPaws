using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.HardDelete;

public record HardDeleteVolunteerCommand(Guid VolunteerId)
    : ICommand
{
    public HardDeleteVolunteerExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId);
}