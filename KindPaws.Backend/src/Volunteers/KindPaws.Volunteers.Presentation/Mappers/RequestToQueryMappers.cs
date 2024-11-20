using KindPaws.Volunteers.Application.Features.Pets.Commands.Add;
using KindPaws.Volunteers.Application.Features.Pets.Commands.DeletePhotos;
using KindPaws.Volunteers.Application.Features.Pets.Commands.SetMainPhoto;
using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateAdditionalInfo;
using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateMainInfo;
using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePosition;
using KindPaws.Volunteers.Application.Features.Pets.Queries.GetPets;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.Create;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateInfo;
using KindPaws.Volunteers.Application.Features.Volunteers.Queries.GetVolunteers;
using KindPaws.Volunteers.Contracts.Requests;

namespace KindPaws.Volunteers.Presentation.Mappers;

public static class RequestToQueryMappers
{
    public static GetPetsQuery ToQuery(this GetPetsRequest request)
        => new(
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection,
            request.SpecieId,
            request.BreedId,
            request.Name,
            request.SupportStatus,
            request.Color,
            request.Age,
            request.PositionFrom,
            request.PositionTo,
            request.VolunteerId);

    public static GetVolunteersQuery ToQuery(this GetVolunteersRequest request)
        => new(
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection);
}