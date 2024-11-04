using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Queries.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid VolunteerId) : IQuery;