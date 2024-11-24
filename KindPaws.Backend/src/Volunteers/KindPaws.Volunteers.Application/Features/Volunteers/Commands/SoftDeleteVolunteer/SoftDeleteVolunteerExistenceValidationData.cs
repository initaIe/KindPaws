using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.SoftDeleteVolunteer;

public record SoftDeleteVolunteerExistenceValidationData(Guid VolunteerId) : IExistenceValidationData;