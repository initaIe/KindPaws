using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Domain.VolunteerRequestManagement.AggregateRoot;
using KindPaws.Users.Domain.VolunteerRequestManagement.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Users.Application.Factories;

public static class VolunteerRequestFactory
{
    public static VolunteerRequest ForceCreateNew(
        Guid requesterUserId,
        string body)
    {
        var volunteerRequestRequesterUserId = UserId.Create(requesterUserId).Value;
        var volunteerRequestBody = VolunteerRequestBody.Create(body).Value;

        return VolunteerRequest.CreateNew(volunteerRequestRequesterUserId, volunteerRequestBody);
    }
}