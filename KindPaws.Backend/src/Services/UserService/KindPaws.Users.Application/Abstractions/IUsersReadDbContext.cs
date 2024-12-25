using KindPaws.Users.Application.Common.DataModels;

namespace KindPaws.Users.Application.Abstractions;

public interface IUsersReadDbContext
{
    IQueryable<UserDataModel> Users { get; }
    IQueryable<ProfileDataModel> Profiles { get; }

    IQueryable<RoleDataModel> Roles { get; }
    // IQueryable<VolunteerRequestDataModel> VolunteerRequests { get; }
}