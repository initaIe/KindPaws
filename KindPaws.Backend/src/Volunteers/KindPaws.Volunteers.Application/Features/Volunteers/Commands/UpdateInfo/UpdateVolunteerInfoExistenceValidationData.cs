using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateInfo;

public record UpdateVolunteerInfoExistenceValidationData(Guid VolunteerId) : IExistenceValidationData;