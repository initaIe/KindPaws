using System.Text.Json;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;
using KindPaws.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KindPaws.Infrastructure.Helpers;

public static class ValueConverters
{
    public static readonly ValueConverter SocialNetworkListConverter =
        new ValueConverter<SocialNetworkList, string>(
            v => JsonSerializer.Serialize(v.SocialNetworks, JsonSerializerOptionsPresets.Default),
            v => new SocialNetworkList(
                JsonSerializer.Deserialize<IEnumerable<SocialNetwork>>(v,
                    JsonSerializerOptionsPresets.Default)
                ?? Enumerable.Empty<SocialNetwork>()));

    public static readonly ValueConverter RequisiteListConverter =
        new ValueConverter<RequisiteList, string>(
            v => JsonSerializer.Serialize(v.Requisites, JsonSerializerOptionsPresets.Default),
            v => new RequisiteList(
                JsonSerializer.Deserialize<IEnumerable<Requisite>>(v,
                    JsonSerializerOptionsPresets.Default)
                ?? Enumerable.Empty<Requisite>()));
}