using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Domain.VolunteerRequestManagement.AggregateRoot;
using KindPaws.Users.Domain.VolunteerRequestManagement.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Configurations.Write;

public class VolunteerRequestConfiguration : IEntityTypeConfiguration<VolunteerRequest>
{
    public void Configure(EntityTypeBuilder<VolunteerRequest> builder)
    {
        // TABLE NAMING
        builder.ToTable("volunteer_requests");

        // ID
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerRequestId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(r => r.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(u => u.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);

        // REQUESTER_USER_ID
        builder.Property(r => r.RequesterUserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value).Value)
            .HasColumnName("requester_user_id")
            .IsRequired();

        // REVIEWER_USER_ID
        builder.Property(r => r.RequesterUserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value).Value)
            .HasColumnName("reviewer_user_id")
            .IsRequired(false);

        // STATUS
        builder.Property(r => r.Status)
            .HasConversion(
                status => status.Value,
                value => VolunteerRequestStatus.Create(value).Value)
            .HasColumnName("status")
            .IsRequired();

        // BODY
        builder.Property(r => r.Body)
            .HasConversion(
                body => body.Value,
                value => VolunteerRequestBody.Create(value).Value)
            .HasColumnName("body")
            .IsRequired();

        // IGNORE
        builder.Ignore(r => r.DomainEvents);
    }
}