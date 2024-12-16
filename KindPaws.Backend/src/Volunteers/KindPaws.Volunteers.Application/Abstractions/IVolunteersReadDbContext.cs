using KindPaws.Volunteers.Application.DataModels;

namespace KindPaws.Volunteers.Application.Abstractions;

public interface IVolunteersReadDbContext
{
    IQueryable<VolunteerDataModel> Volunteers { get; }
    IQueryable<PetDataModel> Pets { get; }
}