using KindPaws.Accounts.Contracts.Dtos;
using KindPaws.Accounts.Domain.Account.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Accounts.Application.Helpers;

public static class AccountsMappers
{
    public static FullNameDto ToDto(this FullName fullName)
        => new(
            fullName.FirstName,
            fullName.LastName,
            fullName.Patronymic);

    public static SocialNetworkDto ToDto(this SocialNetwork socialNetwork)
        => new(
            socialNetwork.Name,
            socialNetwork.Link);
}