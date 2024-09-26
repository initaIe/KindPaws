using KindPaws.Domain.Shared.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.VOs.Constraints;

namespace KindPaws.Domain.Shared.VOs;

public record Description
{
    private Description(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }
    
    public static Result<Description, IEnumerable<string>> Create(string value)
    {
        List<string> errors = [];
        
        value.DefaultValidate(
            DescriptionConstraints.MinLength, 
            DescriptionConstraints.MaxLength)
            .AddErrorIfFailure(errors);
        
        if (errors.Count > 0)
            return errors;

        var description = new Description(value);

        return description;
    }
}