using KindPaws.Core.Abstractions.Database.DbContexts.Interfaces;
using KindPaws.Core.MessageBox.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Core.MessageBox.Abstractions.Interfaces;

public interface IInBoxWriteDbContext : IApplicationDbContext
{
    DbSet<InBoxMessage> InBoxMessages { get; }
}