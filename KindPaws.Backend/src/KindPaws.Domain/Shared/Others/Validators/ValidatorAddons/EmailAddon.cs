namespace KindPaws.Domain.Shared.Others.Validators.ValidatorAddons;

public static class EmailAddon
{
    public const string EmailAddressPattern
        = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
}