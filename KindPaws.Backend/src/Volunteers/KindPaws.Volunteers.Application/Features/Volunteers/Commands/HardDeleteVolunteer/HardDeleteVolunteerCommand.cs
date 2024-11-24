using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.HardDeleteVolunteer;

public record HardDeleteVolunteerCommand(Guid VolunteerId)
    : ICommand
{
    public HardDeleteVolunteerExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId);
}