using KindPaws.VolunteerRequests.Application.DataModels;

namespace KindPaws.VolunteerRequests.Application.Abstractions;

public interface IVolunteerRequestsReadDbContext
{
    public IQueryable<VolunteerRequestDataModel> VolunteerRequests { get; }
}