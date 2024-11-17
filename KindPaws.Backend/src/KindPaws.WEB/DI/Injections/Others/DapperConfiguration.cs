using Dapper;

namespace KindPaws.WEB.DI.Injections.Others;

public static class DapperConfiguration
{
    public static void Configure()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}