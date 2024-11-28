using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.VolunteerRequests.Domain.AggregateRoot;
using KindPaws.VolunteerRequests.Domain.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.VolunteerRequests.Infrastructure.Configurations.Write;

public class VolunteerRequestConfiguration : IEntityTypeConfiguration<VolunteerRequest>
{
    public void Configure(EntityTypeBuilder<VolunteerRequest> builder)
    {
        // TABLE NAMING
        builder.ToTable("volunteer_requests");

        // ID
        builder.HasKey(vr => vr.Id);
        builder.Property(vr => vr.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerRequestId.Create(value).Value)
            .HasColumnName("id");

        // REQUESTER ACCOUNT ID
        builder.Property(vr => vr.RequesterAccountId)
            .HasConversion(
                requesterAccountId => requesterAccountId.Value,
                value => AccountId.Create(value).Value)
            .HasColumnName("requester_account_id")
            .IsRequired();

        // ADMIN REVIEWER ACCOUNT ID
        builder.Property(vr => vr.AdminReviewerAccountId)
            .HasConversion(
                adminReviewerAccountId => adminReviewerAccountId!.Value,
                value => AccountId.Create(value).Value)
            .HasColumnName("admin_reviewer_account_id")
            .IsRequired(false);

        // DISCUSSION ID
        builder.Property(vr => vr.DiscussionId)
            .HasConversion(
                discussionId => discussionId!.Value,
                value => DiscussionId.Create(value).Value)
            .HasColumnName("discussion_id")
            .IsRequired(false);

        // VOLUNTEER INFO
        builder.Property(vr => vr.VolunteerInfo)
            .HasConversion(
                volunteerInfo => volunteerInfo!.Value,
                value => VolunteerInfo.Create(value).Value)
            .HasColumnName("volunteer_info")
            .IsRequired();

        // REJECTION COMMENT
        builder.Property(vr => vr.RejectionComment)
            .HasConversion(
                rejectionComment => rejectionComment!.Value,
                value => RejectionComment.Create(value).Value)
            .HasColumnName("rejection_comment")
            .IsRequired(false);

        // STATUS
        builder.Property(vr => vr.Status)
            .HasConversion(
                status => status!.Value,
                value => VolunteerRequestStatus.Create(value).Value)
            .HasColumnName("status")
            .IsRequired();

        // CREATION TIMESTAMP
        builder.Property(vr => vr.CreationTimestamp)
            .HasConversion(
                creationTimestamp => creationTimestamp.Value,
                value => CreationTimestamp.Create(value).Value)
            .HasColumnName("creation_timestamp")
            .IsRequired();
    }
}