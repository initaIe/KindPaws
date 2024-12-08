using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

public record ProfileDescription
{
    private ProfileDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<ProfileDescription, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return GeneralErrors.General.ValueIsRequired(nameof(ProfileDescription));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                ProfielConstraints.MinLength,
                ProfielConstraints.MaxLength))
            return GeneralErrors.General.ValueOutOfRange();

        return new ProfileDescription(input);
    }
}