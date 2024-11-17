using System.Text.Json;
using Dapper;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Helpers;
using KindPaws.Volunteers.Contracts.Dtos;
using KindPaws.Volunteers.Domain.Entities;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Features.Pets.Queries.GetPetById;

public class GetPetByIdDapperHandler : IQueryHandler<Result<PetDto, ErrorList>, GetPetByIdQuery>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetPetByIdDapperHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<PetDto, ErrorList>> HandleAsync(
        GetPetByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        using var connection = _sqlConnectionFactory.Create();
        connection.Open();

        var builder = new SqlBuilder();

        builder.Where("id = @Id", new { id = query.PetId });

        var selector = builder.AddTemplate(
            """
            SELECT id,
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
            """
        );

        var petResponse = await connection.QueryFirstOrDefaultAsync<(
                Guid id,
                Guid specie_id,
                Guid breed_id,
                string name,
                string support_status,
                string description,
                string color,
                DateTime date_birth,
                string health_details,
                string biometric_details,
                DateTime creation_date_time,
                string photos,
                int position,
                Guid volunteer_id,
                bool is_soft_deleted)>
            (selector.RawSql, selector.Parameters);

        if (petResponse.id == Guid.Empty)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), query.PetId).ToErrorList();

        var healthDetails = JsonSerializer.Deserialize<HealthDetails>(petResponse.health_details)!
            .ToDto();

        var biometricDetails = JsonSerializer.Deserialize<BiometricDetails>(petResponse.biometric_details)!
            .ToDto();

        var photos = JsonSerializer.Deserialize<IEnumerable<PetPhoto>>(petResponse.photos)!
            .Select(p => p.ToDto());

        var dateBirth = DateOnly.FromDateTime(petResponse.date_birth);

        var petDto = new PetDto
        {
            Id = petResponse.id,
            SpecieId = petResponse.specie_id,
            BreedId = petResponse.breed_id,
            Name = petResponse.name,
            SupportStatus = petResponse.support_status,
            Description = petResponse.description,
            Color = petResponse.color,
            Age = dateBirth,
            HealthDetails = healthDetails,
            BiometricDetails = biometricDetails,
            CreationDateTime = petResponse.creation_date_time,
            Position = petResponse.position,
            Photos = photos.ToArray(),
            VolunteerId = petResponse.volunteer_id,
            IsSoftDeleted = petResponse.is_soft_deleted
        };

        return petDto;
    }
}