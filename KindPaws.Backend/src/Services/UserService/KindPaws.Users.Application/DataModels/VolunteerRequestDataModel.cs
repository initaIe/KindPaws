namespace KindPaws.Users.Application.DataModels;

public class VolunteerRequestDataModel
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? LastModifiedAt { get; init; }
    public Guid RequesterUserId { get; init; }
    public Guid? ReviewerUserId { get; init; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Status { get; init; } = null!;

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Body { get; init; } = null!;
}