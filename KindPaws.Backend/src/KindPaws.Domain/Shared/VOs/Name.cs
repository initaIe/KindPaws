using KindPaws.Domain.Shared.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.VOs.Constraints;

namespace KindPaws.Domain.Shared.VOs;

public class Name
{
    private Name(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }
    
    
    public static Result<Name, IEnumerable<string>> Create(string value)
    {
        List<string> errors = [];
        
        value.DefaultValidate(NameConstraints.MinLength, NameConstraints.MaxLength)
            .AddErrorIfFailure(errors);
        
        if (errors.Count > 0)
            return errors;

        var name = new Name(value);

        return name;
    }
}