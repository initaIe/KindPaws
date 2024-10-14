namespace KindPaws.Application.Volunteers.Pet.Add.DTOs;

public record AddPetCommand(
    Guid VolunteerId,
    Guid SpecieId,
    Guid BreedId,
    string Name);