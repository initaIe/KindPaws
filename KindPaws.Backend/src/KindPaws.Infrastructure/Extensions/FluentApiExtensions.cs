using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KindPaws.Infrastructure.Extensions;

public static class FluentApiExtensions
{
    public static PropertyBuilder<TProperty> ToJsonb<TProperty>(this PropertyBuilder<TProperty> builder)
    {
        return builder
            .HasConversion(
                v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                v => JsonSerializer.Deserialize<TProperty>(v, JsonSerializerOptions.Default)!,
                new ValueComparer<TProperty>(
                    (c1, c2) => JsonEquals(c1, c2),
                    c => JsonHashCode(c),
                    c => JsonCopy(c)
                )
            )
            .HasColumnType("jsonb");
    }

    private static bool JsonEquals<T>(T obj1, T obj2)
    {
        if (obj1 == null && obj2 == null) return true;
        if (obj1 == null || obj2 == null) return false;

        return JsonSerializer.Serialize(obj1, JsonSerializerOptions.Default)
               == JsonSerializer.Serialize(obj2, JsonSerializerOptions.Default);
    }

    private static int JsonHashCode<T>(T obj)
    {
        return obj == null ? 0 : JsonSerializer.Serialize(obj).GetHashCode();
    }

    private static T JsonCopy<T>(T obj)
    {
        if (obj == null) return default!;
        var json = JsonSerializer.Serialize(obj, JsonSerializerOptions.Default);
        return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Default)!;
    }
    
    public static PropertyBuilder<TProperty> MapJsonb<TProperty>(this PropertyBuilder<TProperty> propertyBuilder)
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
                JsonSerializer.Deserialize<TProperty>(JsonSerializer.Serialize<TProperty>
                    (v, JsonSerializerOptions.Default), JsonSerializerOptions.Default)!);

        propertyBuilder.HasConversion(converter);
        propertyBuilder.Metadata.SetValueConverter(converter);
        propertyBuilder.Metadata.SetValueComparer(comparer);
        propertyBuilder.HasColumnType("jsonb");

        return propertyBuilder;
    }

    // public static PropertyBuilder<IReadOnlyList<TValueObject>> ValueObjectsCollectionJsonConversion<TValueObject, TDto>(
    //     this PropertyBuilder<IReadOnlyList<TValueObject>> builder,
    //     Func<TValueObject, TDto> toDtoSelector,
    //     Func<TDto, TValueObject> toValueObjectSelector)
    // {
    //     return builder.HasConversion(
    //             valueObjects => SerializeValueObjectsCollection(valueObjects, toDtoSelector),
    //             json => DeserializeDtoCollection(json, toValueObjectSelector),
    //             CreateCollectionValueComparer<TValueObject>())
    //         .HasColumnType("jsonb");
    // }
    //
    // private static string SerializeValueObjectsCollection<TValueObject, TDto>(
    //     IReadOnlyList<TValueObject> valueObjects, Func<TValueObject, TDto> selector)
    // {
    //     var dtos = valueObjects.Select(selector);
    //
    //     return JsonSerializer.Serialize(dtos, JsonSerializerOptions.Default);
    // }
    //
    // private static IReadOnlyList<TValueObject> DeserializeDtoCollection<TValueObject, TDto>(
    //     string json, Func<TDto, TValueObject> selector)
    // {
    //     var dtos = JsonSerializer.Deserialize<IEnumerable<TDto>>(json, JsonSerializerOptions.Default) 
    //                ?? [];
    //
    //     return dtos.Select(selector).ToList();
    // }
    //
    // private static ValueComparer<IReadOnlyList<T>> CreateCollectionValueComparer<T>() =>
    //     new(
    //         (c1, c2) => c1!.SequenceEqual(c2!),
    //         c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
    //         c => c.ToList());
}