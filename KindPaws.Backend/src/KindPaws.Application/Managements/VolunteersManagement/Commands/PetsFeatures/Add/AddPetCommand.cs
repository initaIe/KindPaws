using KindPaws.Application.Abstractions;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.Add;

public record AddPetCommand(
    Guid VolunteerId,
    Guid SpecieId,
    Guid BreedId,
    string Name)
    : ICommand;