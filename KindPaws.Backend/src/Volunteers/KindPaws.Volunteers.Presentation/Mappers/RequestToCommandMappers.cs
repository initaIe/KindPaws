using KindPaws.Volunteers.Application.Features.Pets.Commands.Add;
using KindPaws.Volunteers.Application.Features.Pets.Commands.DeletePhotos;
using KindPaws.Volunteers.Application.Features.Pets.Commands.SetMainPhoto;
using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateAdditionalInfo;
using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateMainInfo;
using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePosition;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.Create;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateInfo;
using KindPaws.Volunteers.Contracts.Requests;

namespace KindPaws.Volunteers.Presentation.Mappers;

public static class RequestToCommandMappers
{
    public static CreateVolunteerCommand ToCommand(this CreateVolunteerRequest request)
        => new(
            request.Description,
            request.Address,
            request.YearsOfExperience,
            request.Requisites);

    public static UpdateVolunteerInfoCommand ToCommand(
        this UpdateVolunteerInfoRequest request,
        Guid volunteerId)
        => new(
            volunteerId,
            request.Description,
            request.Address,
            request.YearsOfExperience,
            request.Requisites);

    public static AddPetCommand ToCommand(
        this AddPetRequest request,
        Guid volunteerId)
        => new(
            volunteerId,
            request.SpecieId,
            request.BreedId,
            request.Name);

    public static UpdatePetMainInfoCommand ToCommand(
        this UpdatePetMainInfoRequest request,
        Guid volunteerId,
        Guid petId)
        => new(
            volunteerId,
            petId,
            request.SpecieId,
            request.BreedId,
            request.Name);

    public static UpdatePetAdditionalInfoCommand ToCommand(
        this UpdatePetAdditionalInfoRequest request,
        Guid volunteerId,
        Guid petId)
        => new(
            volunteerId,
            petId,
            request.SupportStatus,
            request.Description,
            request.Color,
            request.Birthday,
            request.HealthDetails,
            request.BiometricDetails);

    public static UpdatePetPositionCommand ToCommand(
        this UpdatePetPositionRequest request,
        Guid volunteerId,
        Guid petId)
        => new(
            volunteerId,
            petId,
            request.Position);

    public static SetPetMainPhotoCommand ToCommand(
        this SetPetMainPhotoRequest request,
        Guid volunteerId,
        Guid petId)
        => new(
            volunteerId,
            petId,
            request.Path);

    public static DeletePetPhotosCommand ToCommand(
        this DeletePetPhotosRequest request,
        Guid volunteerId,
        Guid petId)
        => new(
            volunteerId,
            petId,
            request.PhotosPaths);
}