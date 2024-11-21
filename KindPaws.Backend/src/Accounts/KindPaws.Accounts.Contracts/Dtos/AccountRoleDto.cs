namespace KindPaws.Accounts.Contracts.Dtos;

public class AccountRoleDto
{
    public Guid Id { get; init; }
    public Guid AccountId { get; init; }
    public Guid RoleId { get; init; }
    public DateTime CreationTimestamp { get; init; }
}