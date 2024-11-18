using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.SharedKernel.Utilities.Helpers;

// TODO: подумать куда его правильно мувнуть!
public static class ValueObjectsHelpers
{
    public static TValueObject? CreateNullableValueObject<TValueObject, TInput>( 
        TInput? value,
        Func<TInput, Result<TValueObject, Error>> createValueObject)
    {
        if (value == null)
            return default;

        var result = createValueObject(value);

        return result.Value;
    }
    
    public static List<TValueObject> CreateNullableValueObjects<TValueObject, TInput>(
        IEnumerable<TInput>? values,
        Func<TInput, Result<TValueObject, Error>> createValueObject)
    {
        var valuesList = values?.ToList();
        
        if (valuesList == null || valuesList.Count == 0)
            return [];

        List<TValueObject> items = [];
        foreach (var value in valuesList)
        {
            var result = createValueObject(value);
            
            items.Add(result.Value);
        }

        return items;
    }
}