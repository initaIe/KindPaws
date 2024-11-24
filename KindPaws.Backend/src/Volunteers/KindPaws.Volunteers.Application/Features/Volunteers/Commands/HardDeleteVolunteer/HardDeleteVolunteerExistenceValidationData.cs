using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.HardDelete;

public record HardDeleteVolunteerExistenceValidationData(Guid VolunteerId)
    : IExistenceValidationData;