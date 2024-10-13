namespace KindPaws.Application.Volunteers.UpdateMainInfo.DTOs;

public record UpdateVolunteerMainInfoRequest(
    Guid VolunteerId,
    UpdateVolunteerMainInfoDTO Dto);