using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Queries.GetPetById;

public record GetPetByIdQuery(Guid PetId) : IQuery;