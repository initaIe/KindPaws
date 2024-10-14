namespace KindPaws.Application.Volunteers.Volunteer.UpdateMainInfo.DTOs;

public record UpdateVolunteerMainInfoRequest(
    Guid VolunteerId,
    UpdateVolunteerMainInfoDTO Dto);