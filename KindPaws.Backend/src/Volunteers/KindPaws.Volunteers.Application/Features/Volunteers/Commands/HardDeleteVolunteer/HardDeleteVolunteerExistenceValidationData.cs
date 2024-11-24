using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.HardDeleteVolunteer;

public record HardDeleteVolunteerExistenceValidationData(Guid VolunteerId)
    : IExistenceValidationData;