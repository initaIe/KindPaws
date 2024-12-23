using KindPaws.Core.OutBox.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Core.OutBox.Abstractions;

public interface IOutBoxWriteDbContext
{
    DbSet<OutBoxMessage> OutBoxMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}