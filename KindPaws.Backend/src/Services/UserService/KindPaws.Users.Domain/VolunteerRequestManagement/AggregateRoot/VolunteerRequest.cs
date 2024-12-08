using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Domain.VolunteerRequestManagement.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Users.Domain.VolunteerRequestManagement.AggregateRoot;

// TODO: ADD SOME MESSAGING/DISSCUSION ETC...
public class VolunteerRequest : AggregateRoot<VolunteerRequestId>
{
    #region EF Core constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private VolunteerRequest(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        VolunteerRequestId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    #endregion

    private VolunteerRequest(
        VolunteerRequestId id,
        CreatedAt createdAt,
        UserId requesterUserId,
        VolunteerRequestBody body)
        : base(id, createdAt)
    {
        RequesterUserId = requesterUserId;
        Body = body;
    }

    public UserId RequesterUserId { get; }
    public UserId? ReviewerUserId { get; private set; }
    public VolunteerRequestStatus Status { get; private set; } = VolunteerRequestStatus.Undefined;
    public VolunteerRequestBody Body { get; private set; }

    #region Properties

    public bool IsTaken => ReviewerUserId != null;

    #endregion

    #region Factory methods

    public static VolunteerRequest CreateNew(
        UserId requesterUserId,
        VolunteerRequestBody body)
    {
        var id = VolunteerRequestId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new VolunteerRequest(
            id,
            createdAt,
            requesterUserId,
            body);
    }

    public static VolunteerRequest Create(
        VolunteerRequestId id,
        CreatedAt createdAt,
        UserId requesterUserId,
        VolunteerRequestBody body)
    {
        return new VolunteerRequest(
            id,
            createdAt,
            requesterUserId,
            body);
    }

    #endregion

    #region CRUD

    public void TakeOnReview(UserId reviewerUserId)
    {
        ReviewerUserId = reviewerUserId;
    }

    public Result<Error> Approve(UserId reviewerUserId)
    {
        if (ReviewerUserId != reviewerUserId)
            return GeneralErrors.General.OperationCanNotBePerformed(
                "Approve request",
                "Reviewer user id is invalid");

        Status = VolunteerRequestStatus.Approved;
        return true;
    }

    public Result<Error> Reject(UserId reviewerUserId)
    {
        if (ReviewerUserId != reviewerUserId)
            return GeneralErrors.General.OperationCanNotBePerformed(
                "Approve request",
                "Reviewer user id is invalid");

        Status = VolunteerRequestStatus.Rejected;
        return true;
    }

    #endregion
}