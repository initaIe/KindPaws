using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.SoftDelete;

public record SoftDeleteVolunteerExistenceValidationData(Guid VolunteerId) : IExistenceValidationData;