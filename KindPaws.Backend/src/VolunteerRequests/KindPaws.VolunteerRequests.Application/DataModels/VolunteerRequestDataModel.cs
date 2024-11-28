namespace KindPaws.VolunteerRequests.Application.DataModels;

public class VolunteerRequestDataModel
{
    public Guid Id { get; init; }
    public Guid RequesterAccountId { get; init; }
    public Guid? AdminReviewerAccountId { get; init; }
    public Guid? DiscussionId { get; init; }
    public string VolunteerInfo { get; init; } = null!;
    public string? RejectionComment { get; init; }
    public string Status { get; init; } = null!;
    public DateTime CreationTimestamp { get; init; }
}