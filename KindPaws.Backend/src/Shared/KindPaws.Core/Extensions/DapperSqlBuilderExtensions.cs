using Dapper;

namespace KindPaws.Core.Extensions;

public static class DapperSqlBuilderExtensions
{
    public static void AddPaginationParameters(this SqlBuilder sqlBuilder, int pageSize, int pageNumber)
    {
        sqlBuilder.AddParameters(new
        {
            @PageSize = pageSize,
            @Offset = (pageNumber - 1) * pageSize
        });
    }

    public static void ApplyFiltration(
        this SqlBuilder sqlBuilder,
        (bool condition, string sql)[] filters,
        (string parameterName, object? parametrValue)[] filterParameters)
    {
        foreach (var filter in filters)
        {
            if (filter.condition)
                sqlBuilder.Where(filter.sql);
        }

        var filterDynamicParameters = new DynamicParameters();

        foreach (var filterParameter in filterParameters)
        {
            if (filterParameter.parametrValue == null || filterParameter.parametrValue.ToString() == "%%")
                continue;

            filterDynamicParameters.Add(filterParameter.parameterName, filterParameter.parametrValue);
        }

        sqlBuilder.AddParameters(filterDynamicParameters);
    }

    public static void AddOrderBy(
        this SqlBuilder sqlBuilder,
        (string, string)[] orderByFields,
        string? orderByField = "",
        string? orderByDirection = "")
    {
        var normalizeOrderByField = NormalizeOrderByField(orderByFields, orderByField);
        var normalizedOrderByDirectionField = NormalizeOrderByDirectionField(orderByDirection);
        sqlBuilder.OrderBy($"{normalizeOrderByField} {normalizedOrderByDirectionField}");
    }

    private static string NormalizeOrderByDirectionField(string? orderDirection)
        => orderDirection?.ToLower() == "desc" ? "desc" : "asc";

    private static string NormalizeOrderByField(
        (string queryString, string dbColumnName)[] orderByFields,
        string? orderByField)
    {
        var firstOrDefaultOrderByTuple = orderByFields.FirstOrDefault(
            x => x.queryString.Equals(orderByField, StringComparison.CurrentCultureIgnoreCase));

        if (string.IsNullOrWhiteSpace(firstOrDefaultOrderByTuple.dbColumnName))
            return "id";

        return firstOrDefaultOrderByTuple.dbColumnName;
    }
}