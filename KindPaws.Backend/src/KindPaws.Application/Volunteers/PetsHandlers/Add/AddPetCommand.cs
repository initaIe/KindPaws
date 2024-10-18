namespace KindPaws.Application.Volunteers.PetsHandlers.Add;

public record AddPetCommand(
    Guid VolunteerId,
    Guid SpecieId,
    Guid BreedId,
    string Name);