using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.SoftDeleteVolunteer;

public record SoftDeleteVolunteerCommand(Guid VolunteerId)
    : ICommand;