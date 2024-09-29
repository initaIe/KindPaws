using KindPaws.Domain.Shared.Others.Helpers;

namespace KindPaws.Domain.Shared.Others.Validators;

public static class DateOnlyValidator
{
    public static Result<string> PastDateOnlyValidate(this DateOnly date)
    {
        var dateNow = DateOnlyHelper.GetDateOnlyNow();
        if (dateNow < date)
            return "Date can not be in future.";

        return true;
    }
}