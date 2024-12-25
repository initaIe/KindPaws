using KindPaws.Users.Application.Common.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Persistence.Configurations.Read;

public class VolunteerRequestDataModelConfiguration : IEntityTypeConfiguration<VolunteerRequestDataModel>
{
    public void Configure(EntityTypeBuilder<VolunteerRequestDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("volunteer_requests");

        // ID
        builder.Property(volunteerRequest => volunteerRequest.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(volunteerRequest => volunteerRequest.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(volunteerRequest => volunteerRequest.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // REQUESTER_USER_ID
        builder.Property(volunteerRequest => volunteerRequest.RequesterUserId)
            .HasColumnName("requester_user_id");

        // REQUESTER_USER_ID
        builder.Property(volunteerRequest => volunteerRequest.ReviewerUserId)
            .HasColumnName("reviewer_user_id");

        // STATUS
        builder.Property(volunteerRequest => volunteerRequest.Status)
            .HasColumnName("status");

        // BODY
        builder.Property(volunteerRequest => volunteerRequest.Body)
            .HasColumnName("body");
    }
}