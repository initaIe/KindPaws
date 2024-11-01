using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

namespace KindPaws.Application.DTOs;

public record RequisiteDTO(
    string Name,
    string Description)
{
    public static RequisiteDTO GetFromDomainModel(Requisite requisite)
        => new(requisite.Name, requisite.Description);
}