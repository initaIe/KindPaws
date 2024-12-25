using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Domain.VolunteerRequestManagement.AggregateRoot;
using KindPaws.Users.Domain.VolunteerRequestManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Users.Domain.VolunteerRequestManagement.ValueObjectsManagement.ValueObjectsConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Persistence.Configurations.Write;

public class VolunteerRequestConfiguration : IEntityTypeConfiguration<VolunteerRequest>
{
    public void Configure(EntityTypeBuilder<VolunteerRequest> builder)
    {
        // TABLE NAMING
        builder.ToTable("volunteer_requests");

        // ID
        builder.HasKey(volunteerRequest => volunteerRequest.Id);
        builder.Property(volunteerRequest => volunteerRequest.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerRequestId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(volunteerRequest => volunteerRequest.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(volunteerRequest => volunteerRequest.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);

        // REQUESTER_USER_ID
        builder.Property(volunteerRequest => volunteerRequest.RequesterUserId)
            .HasConversion(
                requesterUserId => requesterUserId!.Value,
                value => UserId.Create(value).Value)
            .HasColumnName("requester_user_id")
            .IsRequired();

        // REQUESTER_USER_ID
        builder.Property(volunteerRequest => volunteerRequest.ReviewerUserId)
            .HasConversion(
                reviewerUserId => reviewerUserId!.Value,
                value => UserId.Create(value).Value)
            .HasColumnName("reviewer_user_id")
            .IsRequired(false);

        // STATUS
        builder.Property(volunteerRequest => volunteerRequest.Status)
            .HasConversion(
                status => status!.Value,
                value => VolunteerRequestStatus.Create(value).Value)
            .HasMaxLength(VolunteerRequestStatusConstraints.MaxLength)
            .HasColumnName("status")
            .IsRequired();

        // BODY
        builder.Property(volunteerRequest => volunteerRequest.Body)
            .HasConversion(
                body => body!.Value,
                value => VolunteerRequestBody.Create(value).Value)
            .HasMaxLength(VolunteerRequestBodyConstraints.MaxLength)
            .HasColumnName("body")
            .IsRequired();
    }
}