using KindPaws.Core.Abstractions.Database.DbContexts.Interfaces;
using KindPaws.Core.MessageBox.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Core.MessageBox.Abstractions.Interfaces;

public interface IOutBoxWriteDbContext : IApplicationDbContext
{
    DbSet<OutBoxMessage> OutBoxMessages { get; }
}