using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.DTOs;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Application.Managements.VolunteersManagement.Queries.PetsFeatures.GetPetById;

public class GetPetByIdHandler : IQueryHandler<Result<PetDTO, ErrorList>, GetPetByIdQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetPetByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<PetDTO, ErrorList>> HandleAsync(
        GetPetByIdQuery query,
        CancellationToken cancellationToken)
    {
        var petsQuery = _readDbContext.Pets;

        // TODO add validation, filtration, sort and logger

        var petId = PetId.Create(query.PetId).Value;

        var pet = await petsQuery
            .SingleOrDefaultAsync(v => v.Id == query.PetId, cancellationToken);

        if (pet == null)
            return Errors.General.RecordNotFound(
                    nameof(Pet),
                    nameof(PetId),
                    petId.Value)
                .ToErrorList();

        return pet;
    }
}