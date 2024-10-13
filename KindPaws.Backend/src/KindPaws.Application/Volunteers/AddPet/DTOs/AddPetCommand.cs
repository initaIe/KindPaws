namespace KindPaws.Application.Volunteers.AddPet.DTOs;

public record AddPetCommand(
    Guid VolunteerId,
    Guid SpecieId,
    Guid BreedId,
    string Name);