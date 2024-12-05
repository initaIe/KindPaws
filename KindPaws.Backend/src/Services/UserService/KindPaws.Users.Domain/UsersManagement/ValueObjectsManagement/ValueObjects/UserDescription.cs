using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

public record UserDescription
{
    private UserDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<UserDescription, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(UserDescription));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                UserNameConstraints.MinLength,
                UserNameConstraints.MaxLength))
            return Errors.General.ValueOutOfRange();

        return new UserDescription(input);
    }
}