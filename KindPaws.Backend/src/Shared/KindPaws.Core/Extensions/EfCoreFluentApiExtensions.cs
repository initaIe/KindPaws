using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KindPaws.Core.Extensions;

public static class EfCoreFluentApiExtensions
{
    public static PropertyBuilder<TProperty> HasJsonConversion<TProperty>(
        this PropertyBuilder<TProperty> propertyBuilder)
    {
        var converter = new ValueConverter<TProperty, string>(
            v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
            v => JsonSerializer.Deserialize<TProperty>(v, JsonSerializerOptions.Default)!);

        var comparer = new ValueComparer<TProperty>(
            (l, r) =>
                JsonSerializer.Serialize<TProperty>(l!, JsonSerializerOptions.Default)
                == JsonSerializer.Serialize<TProperty>(r!, JsonSerializerOptions.Default),
            v =>
                v == null ? 0 : JsonSerializer.Serialize<TProperty>(v, JsonSerializerOptions.Default).GetHashCode(),
            v =>
                JsonSerializer.Deserialize<TProperty>(JsonSerializer.Serialize
                    (v, JsonSerializerOptions.Default), JsonSerializerOptions.Default)!);

        propertyBuilder.HasConversion(converter);
        propertyBuilder.Metadata.SetValueConverter(converter);
        propertyBuilder.Metadata.SetValueComparer(comparer);
        propertyBuilder.HasColumnType("jsonb");

        return propertyBuilder;
    }
    
    public static PropertyBuilder<IReadOnlyList<TElement>> HasUuidArrayConversion<TElement>(
        this PropertyBuilder<IReadOnlyList<TElement>> propertyBuilder,
        Func<TElement, Guid> toGuidSelector,
        Func<Guid, TElement> fromGuidSelector)
    {
        var converter = new ValueConverter<IReadOnlyList<TElement>, Guid[]>(
            v => v.Select(toGuidSelector).ToArray(),
            v => v.Select(fromGuidSelector).ToList()
        );

        var comparer = new ValueComparer<IReadOnlyList<TElement>>(
            (l, r) => l!.SequenceEqual(r!),
            v => v.Aggregate(0, (hash, element) => hash ^ toGuidSelector(element).GetHashCode()),
            v => v.Select(toGuidSelector).Select(fromGuidSelector).ToList()
        );

        propertyBuilder.HasConversion(converter);
        propertyBuilder.Metadata.SetValueComparer(comparer);
        propertyBuilder.HasColumnType("uuid[]");

        return propertyBuilder;
    }

}