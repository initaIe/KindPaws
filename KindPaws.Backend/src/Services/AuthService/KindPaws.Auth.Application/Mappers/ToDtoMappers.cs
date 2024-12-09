using KindPaws.Auth.Contracts.Dtos;
using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Auth.Application.Mappers;

public static class ToDtoMappers
{
    public static RefreshSessionDto ToDto(this RefreshSession refreshSession)
        => new RefreshSessionDto
        {
            CreatedAt = refreshSession.CreatedAt.Value,
            Jti = refreshSession.Jti.Value,
            RefreshToken = refreshSession.RefreshToken.Value,
            ExpiresAt = refreshSession.ExpiresAt.Value
        };

    public static List<RefreshSessionDto> ToDtoCollection(this IEnumerable<RefreshSession> refreshSessions)
        => refreshSessions.Select(rs => rs.ToDto()).ToList();
}