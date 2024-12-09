using KindPaws.Users.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Configurations.Read;

public class VolunteerRequestDataModelConfiguration : IEntityTypeConfiguration<VolunteerRequestDataModel>
{
    public void Configure(EntityTypeBuilder<VolunteerRequestDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("volunteer_requests");

        // ID
        builder.Property(r => r.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(r => r.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(u => u.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // REQUESTER_USER_ID
        builder.Property(r => r.RequesterUserId)
            .HasColumnName("requester_user_id");

        // REVIEWER_USER_ID
        builder.Property(r => r.RequesterUserId)
            .HasColumnName("reviewer_user_id");

        // STATUS
        builder.Property(r => r.Status)
            .HasColumnName("status");

        // BODY
        builder.Property(r => r.Body)
            .HasColumnName("body");
    }
}