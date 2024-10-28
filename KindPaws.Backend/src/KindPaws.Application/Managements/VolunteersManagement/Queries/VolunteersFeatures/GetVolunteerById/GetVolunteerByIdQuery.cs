using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid VolunteerId) : IQuery;