using KindPaws.Volunteers.Application.DataModels;
using KindPaws.Volunteers.Contracts.Dtos;

namespace KindPaws.Volunteers.Application.Abstractions;

public interface IVolunteersReadDbContext
{
    IQueryable<VolunteerDataModel> Volunteers { get; }
    IQueryable<PetDataModel> Pets { get; }
}