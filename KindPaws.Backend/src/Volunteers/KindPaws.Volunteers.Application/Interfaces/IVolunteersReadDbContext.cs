using KindPaws.Volunteers.Contracts.Dtos;

namespace KindPaws.Volunteers.Application.Interfaces;

public interface IVolunteersReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<PetDto> Pets { get; }
}