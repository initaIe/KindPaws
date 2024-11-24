using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.SoftDelete;

public record SoftDeleteVolunteerCommand(Guid VolunteerId)
    : ICommand
{
    public SoftDeleteVolunteerExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId);
}