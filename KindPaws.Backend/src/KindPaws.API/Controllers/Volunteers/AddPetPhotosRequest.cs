namespace KindPaws.API.Controllers.Volunteers;

public record AddPetPhotosRequest(
    IFormFileCollection Photos);