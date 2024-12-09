using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Pets.Domain.VolunteersManagement.Events;

public record PetDeletedDomainEvent(
    VolunteerId VolunteerId,
    Guid PetId)
    : IDomainEvent;