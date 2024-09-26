namespace KindPaws.Domain.Shared.Others.Validators.ValidatorSettings;

public static class EmailSettings
{
    public const string EmailAddressPattern
        = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
}