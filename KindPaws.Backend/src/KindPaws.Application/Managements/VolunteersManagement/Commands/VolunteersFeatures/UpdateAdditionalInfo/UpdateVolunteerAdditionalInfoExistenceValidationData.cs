using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateAdditionalInfo;

public record UpdateVolunteerAdditionalInfoExistenceValidationData(Guid VolunteerId)
    : IExistenceValidationData;