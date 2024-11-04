using KindPaws.Core.Dtos;

namespace KindPaws.Volunteers.Application.Interfaces;

public interface IVolunteersReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<PetDto> Pets { get; }
}