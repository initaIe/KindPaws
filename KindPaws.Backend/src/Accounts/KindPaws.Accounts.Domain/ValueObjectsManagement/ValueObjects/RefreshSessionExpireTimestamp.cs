using System.Runtime.InteropServices.JavaScript;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;

public class RefreshSessionExpireTimestamp
{
    private RefreshSessionExpireTimestamp(DateTime value)
    {
        Value = value;
    }

    public DateTime Value { get; }

    public static Result<RefreshSessionExpireTimestamp, Error> Create(DateTime input)
    {
        if (input < DateTime.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(RefreshSessionExpireTimestamp));
        
        return new RefreshSessionExpireTimestamp(input);
    }
}