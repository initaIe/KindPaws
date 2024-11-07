using Dapper;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Dtos;
using KindPaws.Core.Extensions;
using KindPaws.Core.Models;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Species.Application.Features.Breeds.Queries.GetBreeds;

public class GetBreedsDapperHandler: IQueryHandler<PagedList<BreedDto>, GetBreedsQuery>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetBreedsDapperHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<PagedList<BreedDto>> HandleAsync(
        GetBreedsQuery query,
        CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.Create();
        connection.Open();

        var builder = new SqlBuilder();

        if (!string.IsNullOrWhiteSpace(query.Name))
            builder.Where("name LIKE @Name", new { name = $"%{query.Name}%" });
        if (query.SpecieId != null && !GuidValidator.IsEmpty(query.SpecieId!.Value))
            builder.Where("specie_id = @SpecieId", new { specie_id = query.SpecieId });
        
        string orderBy = query.SortBy?.ToLower() switch
        {
            "name" => "name",
            "specieid" => "specieid",
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
            SELECT COUNT(*) FROM species.breeds
            /**where**/
            """
        );
        var selector = builder.AddTemplate(
            """
            SELECT id, name, description, specie_id, is_soft_deleted FROM species.breeds
            /**where**/
            /**orderby**/
            LIMIT @PageSize
            OFFSET @Offset
            """
        );

        var totalCount = await connection.ExecuteScalarAsync<long>(counter.RawSql, counter.Parameters);
        var breeds = await connection.QueryAsync<BreedDto>(selector.RawSql, selector.Parameters);

        return new PagedList<BreedDto>
        {
            TotalCount = totalCount,
            Items = breeds.ToList(),
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }
}