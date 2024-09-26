using KindPaws.Domain.Shared.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Shared.VOs;

public class Title
{
    private Title(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }
    
    public static Result<Title, IEnumerable<string>> Create(string value)
    {
        List<string> errors = [];
        
        value.DefaultValidate(
                MinLengthConstraints.MinLengthOne, 
                MaxLengthConstraints.MaxLengthExtraSmall)
            .AddErrorIfFailure(errors);
        
        if (errors.Count > 0)
            return errors;

        var title = new Title(value);

        return title;
    }
}