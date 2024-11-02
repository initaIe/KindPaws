using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Queries.PetsFeatures.GetPetById;

public record GetPetByIdQuery(Guid PetId) : IQuery;