namespace KindPaws.Application.Volunteers.Handlers.UpdateMainInfo.DTOs;

public record UpdateVolunteerMainInfoRequest(
    Guid VolunteerId,
    UpdateVolunteerMainInfoDTO Dto);