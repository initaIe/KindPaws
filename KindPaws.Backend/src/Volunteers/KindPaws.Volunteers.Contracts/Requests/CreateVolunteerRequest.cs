using KindPaws.Core.Dtos;

namespace KindPaws.Volunteers.Contracts.Requests;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string EmailAddress,
    string PhoneNumber);