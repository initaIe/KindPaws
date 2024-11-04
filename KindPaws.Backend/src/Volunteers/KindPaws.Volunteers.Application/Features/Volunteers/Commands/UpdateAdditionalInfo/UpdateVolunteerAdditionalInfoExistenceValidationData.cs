using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateAdditionalInfo;

public record UpdateVolunteerAdditionalInfoExistenceValidationData(Guid VolunteerId)
    : IExistenceValidationData;