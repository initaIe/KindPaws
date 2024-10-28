using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId)
    : ICommand
{
    public DeleteVolunteerExistenceCheckData ToExistenceCheckData()
    {
        return new DeleteVolunteerExistenceCheckData(VolunteerId);
    }
}