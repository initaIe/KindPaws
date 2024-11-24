using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateInfoVolunteer;

public record UpdateVolunteerInfoExistenceValidationData(Guid VolunteerId) : IExistenceValidationData;