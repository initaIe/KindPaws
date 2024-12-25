using KindPaws.Core.MessageBox.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Core.MessageBox.Abstractions.Interfaces;

public interface IInBoxWriteDbContext
{
    DbSet<InBoxMessage> InBoxMessages { get; }
}