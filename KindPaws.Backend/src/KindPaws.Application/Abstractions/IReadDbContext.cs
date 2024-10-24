using KindPaws.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Application.Abstractions;

public interface IReadDbContext
{
    DbSet<VolunteerDTO> Volunteers { get; }
    DbSet<SpecieDTO> Species { get; }
}