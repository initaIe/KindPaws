using KindPaws.VolunteerRequests.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.VolunteerRequests.Infrastructure.Configurations.Read;

public class VolunteerRequestDtoConfiguration : IEntityTypeConfiguration<VolunteerRequestDataModel>
{
    public void Configure(EntityTypeBuilder<VolunteerRequestDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("volunteer_requests");

        // ID
        builder.Property(vr => vr.Id)
            .HasColumnName("id");

        // REQUESTER ACCOUNT ID
        builder.Property(vr => vr.RequesterAccountId)
            .HasColumnName("requester_account_id");

        // ADMIN REVIEWER ACCOUNT ID
        builder.Property(vr => vr.AdminReviewerAccountId)
            .HasColumnName("admin_reviewer_account_id");

        // DISCUSSION ID
        builder.Property(vr => vr.DiscussionId)
            .HasColumnName("discussion_id");

        // VOLUNTEER INFO
        builder.Property(vr => vr.VolunteerInfo)
            .HasColumnName("volunteer_info");

        // REJECTION COMMENT
        builder.Property(vr => vr.RejectionComment)
            .HasColumnName("rejection_comment");

        // STATUS
        builder.Property(vr => vr.Status)
            .HasColumnName("status");

        // CREATION TIMESTAMP
        builder.Property(vr => vr.CreationTimestamp)
            .HasColumnName("creation_timestamp");
    }
}