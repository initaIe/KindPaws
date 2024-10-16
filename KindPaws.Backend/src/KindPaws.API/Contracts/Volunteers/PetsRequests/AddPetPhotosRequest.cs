namespace KindPaws.API.Contracts.Volunteers.PetsRequests;

public record AddPetPhotosRequest(
    IFormFileCollection Photos);