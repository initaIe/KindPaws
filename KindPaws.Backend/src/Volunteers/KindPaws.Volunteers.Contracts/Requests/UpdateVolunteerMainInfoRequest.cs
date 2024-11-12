using KindPaws.Core.Dtos;

namespace KindPaws.Volunteers.Contracts.Requests;

public record UpdateVolunteerMainInfoRequest(
    FullNameDto FullName,
    string EmailAddress,
    string PhoneNumber);