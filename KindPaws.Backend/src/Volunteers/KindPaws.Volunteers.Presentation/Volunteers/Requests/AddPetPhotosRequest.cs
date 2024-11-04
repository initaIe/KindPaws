using Microsoft.AspNetCore.Http;

namespace KindPaws.Volunteers.Presentation.Volunteers.Requests;

public record AddPetPhotosRequest(IFormFileCollection Photos);