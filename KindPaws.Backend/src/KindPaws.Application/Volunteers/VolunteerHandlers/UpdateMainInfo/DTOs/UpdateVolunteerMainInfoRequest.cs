namespace KindPaws.Application.Volunteers.VolunteerHandlers.UpdateMainInfo.DTOs;

public record UpdateVolunteerMainInfoRequest(
    Guid VolunteerId,
    UpdateVolunteerMainInfoDTO Dto);