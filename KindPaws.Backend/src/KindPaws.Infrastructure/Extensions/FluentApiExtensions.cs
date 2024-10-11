using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KindPaws.Infrastructure.Extensions;

public static class FluentApiExtensions
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        IncludeFields = true,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,

    };
     public static PropertyBuilder<TProperty> HasValueJsonConverter<TProperty>(this PropertyBuilder<TProperty> builder)
    {
        return builder.HasConversion(
            v => JsonSerializer.Serialize(v, JsonSerializerOptions), // Сериализация в JSON
            v => JsonSerializer.Deserialize<TProperty>(v, JsonSerializerOptions)!, // Десериализация из JSON
            new ValueComparer<TProperty>(
                (c1, c2) => JsonEquals(c1, c2), // Сравнение значений
                c => JsonHashCode(c),          // Генерация хеш-кода
                c => JsonCopy(c)               // Создание копии
            )
        );
    }

    // Метод для сравнения двух объектов
    private static bool JsonEquals<T>(T obj1, T obj2)
    {
        if (obj1 == null && obj2 == null) return true;
        if (obj1 == null || obj2 == null) return false;

        return JsonSerializer.Serialize(obj1) == JsonSerializer.Serialize(obj2);
    }

    // Метод для генерации хеш-кода объекта
    private static int JsonHashCode<T>(T obj)
    {
        if (obj == null) return 0;
        return JsonSerializer.Serialize(obj).GetHashCode();
    }

    // Метод для создания копии объекта
    private static T JsonCopy<T>(T obj)
    {
        if (obj == null) return default!;
        var json = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T>(json)!;
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