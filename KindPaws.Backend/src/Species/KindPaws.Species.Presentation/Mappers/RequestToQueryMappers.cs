using KindPaws.Species.Application.Features.Breeds.Commands.Add;
using KindPaws.Species.Application.Features.Breeds.Queries.GetBreeds;
using KindPaws.Species.Application.Features.Species.Commands.Create;
using KindPaws.Species.Application.Features.Species.Queries.GetSpecies;
using KindPaws.Species.Contracts.Requests;

namespace KindPaws.Species.Presentation.Mappers;

public static class RequestToQueryMappers
{
    public static GetBreedsQuery ToQuery(this GetBreedsRequest request)
        => new(
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection,
            request.SpecieId,
            request.Name);

    public static GetSpeciesQuery ToQuery(this GetSpeciesRequest request)
        => new(
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection,
            request.Name);
}