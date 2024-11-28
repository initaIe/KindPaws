using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.VolunteerRequests.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.VolunteerRequests.Domain.AggregateRoot;

public class VolunteerRequest : IEntity<VolunteerRequestId>
{
    // ef core
    private VolunteerRequest()
    {
    }

    public VolunteerRequest(
        VolunteerRequestId id,
        AccountId requesterAccountId,
        VolunteerInfo volunteerInfo,
        VolunteerRequestStatus status,
        CreationTimestamp creationTimestamp)
    {
        Id = id;
        RequesterAccountId = requesterAccountId;
        VolunteerInfo = volunteerInfo;
        Status = status;
        CreationTimestamp = creationTimestamp;
    }

    public VolunteerRequestId Id { get; private set; }
    public AccountId RequesterAccountId { get; private set; }
    public AccountId? AdminReviewerAccountId { get; private set; }
    public DiscussionId? DiscussionId { get; private set; }
    public VolunteerInfo VolunteerInfo { get; private set; }
    public RejectionComment? RejectionComment { get; private set; }
    public VolunteerRequestStatus Status { get; private set; } 
    public CreationTimestamp CreationTimestamp { get; private set; }

    #region Factory methods

    public static VolunteerRequest CreateNew(
        AccountId requesterAccountId,
        VolunteerInfo volunteerInfo,
        VolunteerRequestStatus status)
    {
        var id = VolunteerRequestId.CreateRandom();
        var creationTimestamp = CreationTimestamp.CreateNew();

        return new VolunteerRequest(
            id,
            requesterAccountId,
            volunteerInfo,
            status,
            creationTimestamp);
    }

    #endregion

    #region CRUD

    public void TakeRequestOnReview(AccountId adminReviewerAccountId)
    {
        AdminReviewerAccountId = adminReviewerAccountId;
    }

    public void SendOnRevision(RejectionComment rejectionComment)
    {
        Status = VolunteerRequestStatus.RevisionRequired;
        RejectionComment = rejectionComment;
    }

    public void Approve()
    {
        Status = VolunteerRequestStatus.Approved;
    }

    public void Reject()
    {
        Status = VolunteerRequestStatus.Rejected;
    }

    #endregion
}