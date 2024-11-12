namespace KindPaws.Volunteers.Contracts.Requests;

public record DeletePetPhotosRequest(IEnumerable<string> PhotosPaths);