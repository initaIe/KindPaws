namespace KindPaws.Domain.ValidationRules;

public static class FullNameRules
{
    public const int MinNameLength = 1;
    public const int MaxNameLength = 100;

    public const int MinLastNameLength = 1;
    public const int MaxLastNameLength = 100;

    public const int MinPatronymicLength = 1;
    public const int MaxPatronymicLength = 100;
}