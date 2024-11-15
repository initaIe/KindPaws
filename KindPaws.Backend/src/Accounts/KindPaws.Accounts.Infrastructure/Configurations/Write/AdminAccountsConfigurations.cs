using KindPaws.Accounts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Write;

public class AdminAccountsConfigurations : IEntityTypeConfiguration<AdminAccount>
{
    public void Configure(EntityTypeBuilder<AdminAccount> builder)
    {
        // ID
        builder.HasKey(aa => aa.Id);

        // 1-1 relation
        builder.HasOne(aa => aa.User)
            .WithOne()
            .HasForeignKey<AdminAccount>(aa => aa.UserId);

        // FULLNAME
        builder.ComplexProperty(v => v.FullName, fb =>
        {
            fb.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasColumnType("citext")
                .IsRequired();

            fb.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasColumnType("citext")
                .IsRequired();

            fb.Property(x => x.Patronymic)
                .HasColumnName("patronymic")
                .HasColumnType("citext")
                .IsRequired(false);
        });
    }
}