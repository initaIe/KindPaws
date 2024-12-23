using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Application.DataModels;
using KindPaws.Volunteers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Pets.Queries.GetPetById;

public class GetPetByIdHandler : IQueryHandler<Result<PetDataModel, ErrorList>, GetPetByIdQuery>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public GetPetByIdHandler(IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<PetDataModel, ErrorList>> HandleAsync(
        GetPetByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var petsQuery = _readDbContext.Pets;

        var petId = PetId.Create(query.PetId).Value;

        var pet = await petsQuery
            .FirstOrDefaultAsync(v => v.Id == query.PetId, cancellationToken);

        if (pet == null)
            return ErrorsGeneral.RecordNotFound(
                    nameof(Pet),
                    nameof(PetId),
                    petId.Value)
                .ToErrorList();

        return pet;
    }
}