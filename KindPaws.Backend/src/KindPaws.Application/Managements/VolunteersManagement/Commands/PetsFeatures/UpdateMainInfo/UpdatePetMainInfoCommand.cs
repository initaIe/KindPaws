namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateMainInfo;

public record UpdatePetMainInfoCommand(
    Guid VolunteerId,
    Guid PetId,
    Guid SpecieId,
    Guid BreedId,
    string Name);