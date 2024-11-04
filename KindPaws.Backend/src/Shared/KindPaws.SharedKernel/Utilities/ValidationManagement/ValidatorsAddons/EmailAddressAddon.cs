namespace KindPaws.SharedKernel.Utilities.ValidationManagement.ValidatorsAddons;

public static class EmailAddressAddon
{
    public const string EmailAddressPattern
        = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
}