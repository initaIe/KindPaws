using Dapper;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.Core.Models;
using KindPaws.SharedKernel.Enums;
using KindPaws.Species.Contracts.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Species.Application.Features.Species.Queries.GetSpecies;

public class GetSpeciesDapperHandler : IQueryHandler<PagedList<SpecieDto>, GetSpeciesQuery>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetSpeciesDapperHandler([FromKeyedServices(Modules.Species)] ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<PagedList<SpecieDto>> HandleAsync(
        GetSpeciesQuery query,
        CancellationToken cancellationToken = default)
    {
        using var connection = _sqlConnectionFactory.Create();
        connection.Open();

        var builder = new SqlBuilder();

        if (!string.IsNullOrWhiteSpace(query.Name))
            builder.Where("name LIKE @Name", new { name = $"%{query.Name}%" });

        string orderBy = query.SortBy?.ToLower() switch
        {
            "name" => "name",
            _ => "id"
        };

        string orderDirection = query.SortDirection?.ToLower() switch
        {
            "desc" => "desc",
            _ => "asc"
        };

        builder.OrderBy($"{orderBy} {orderDirection}");
        builder.AddPaginationParameters(query.PageSize, query.PageNumber);

        var counter = builder.AddTemplate(
            """
            SELECT COUNT(*) FROM species.species
            /**where**/
            """
        );
        var selector = builder.AddTemplate(
            """
            SELECT id, name, description, is_soft_deleted FROM species.species
            /**where**/
            /**orderby**/
            LIMIT @PageSize
            OFFSET @Offset
            """
        );

        var totalCount = await connection.ExecuteScalarAsync<long>(counter.RawSql, counter.Parameters);
        var species = await connection.QueryAsync<SpecieDto>(selector.RawSql, selector.Parameters);

        return new PagedList<SpecieDto>
        {
            TotalCount = totalCount,
            Items = species.ToList(),
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }
}