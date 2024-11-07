using Dapper;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Dtos;
using KindPaws.Core.Dtos.DapperDtos;
using KindPaws.Core.Extensions;
using KindPaws.Core.Models;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Volunteers.Application.Features.Pets.Queries.GetPets;

public class GetPetsDapperHandler : IQueryHandler<PagedList<PetDto>, GetPetsQuery>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetPetsDapperHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<PagedList<PetDto>> HandleAsync(
        GetPetsQuery query,
        CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.Create();
        connection.Open();

        var builder = new SqlBuilder();
        
        var orderByFieldsTuples = new (string queryOrderByField, string dbColumnName)[]
        {
            ("specieid", "specieId"),
            ("breedid", "breedId"),
            ("name", "name"),
            ("supportstatus", "support_status"),
            ("color", "color"),
            ("age", "date_birth"),
            ("position", "position"),
            ("volunteerid", "volunteerId"),
        };
        
        var filterTuples = new(bool condition, string sql)[]
        {
            (query.SpecieId != null && !GuidValidator.IsEmpty(query.SpecieId!.Value), "specie_id = @SpecieId"),
            (query.BreedId != null && !GuidValidator.IsEmpty(query.BreedId!.Value), "breed_id = @BreedId"),
            (!string.IsNullOrWhiteSpace(query.Name), "name LIKE @Name"),
            (!string.IsNullOrWhiteSpace(query.SupportStatus), "support_status LIKE @SupportStatus"),
            (!string.IsNullOrWhiteSpace(query.Color), "color LIKE @Color"),
            (query.Age != null, "date_birth = @DateBirth"),
            (query.VolunteerId != null && !GuidValidator.IsEmpty(query.VolunteerId!.Value), "volunteer_id = @VolunteerId"),
            (query.PositionFrom != null, "position >= @PositionFrom"),
            (query.PositionTo != null, "position <= @PositionTo")
        };
        
        var filterParameterTuples = new(string parameterName, object? parameterValue)[]
        {
            ("@SpecieId", query.SpecieId),
            ("@BreedId", query.BreedId),
            ("@Name", $"%{query.Name}%"),
            ("@SupportStatus", query.SupportStatus),
            ("@Color", query.Color),
            ("@DateBirth", query.Age),
            ("@VolunteerId", query.VolunteerId),
            ("@PositionFrom", query.PositionFrom),
            ("@PositionTo", query.PositionTo)
        };
        
        builder.ApplyFiltration(filterTuples, filterParameterTuples);
        builder.AddOrderBy(orderByFieldsTuples, query.SortBy, query.SortDirection);
        builder.AddPaginationParameters(query.PageSize, query.PageNumber);

        var counter = builder.AddTemplate(
            """
            SELECT COUNT(*) FROM volunteers.pets
            /**where**/
            """
        );
        
        var selector = builder.AddTemplate(
            """
            SELECT
            id,
            specie_id,
            breed_id,
            name,
            support_status,
            description,
            color,
            date_birth,
            health_details,
            biometric_details,
            creation_date_time,
            photos, 
            position, 
            volunteer_id,
            is_soft_deleted 
            FROM volunteers.pets
            /**where**/
            /**orderby**/
            LIMIT @PageSize
            OFFSET @Offset
            """
        );
        
        var totalCount = await connection.ExecuteScalarAsync<long>(counter.RawSql, counter.Parameters);
        var petDapperDtos = await connection.QueryAsync<PetDapperDto>
            (selector.RawSql, selector.Parameters);

        var petDtos = petDapperDtos.Select(p => p.ToDto());

        return new PagedList<PetDto>
        {
            TotalCount = totalCount,
            Items = petDtos.ToList(),
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }
}