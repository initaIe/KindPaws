using KindPaws.VolunteerRequests.Contracts.Dtos;

namespace KindPaws.VolunteerRequests.Application.Abstractions;

public interface IVolunteerRequestsReadDbContext
{
    public IQueryable<VolunteerRequestDto> VolunteerRequests { get; }
}