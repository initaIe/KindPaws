using KindPaws.Application.Providers;
using KindPaws.Application.Volunteers.AddPet.DTOs;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.AddPet;

public class AddPetHandler
{
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileProvider _fileProvider;

    public AddPetHandler(
        ILogger<AddPetHandler> logger,
        IVolunteersRepository volunteersRepository, 
        IFileProvider fileProvider)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _fileProvider = fileProvider;
    }

    public async Task<Result<Guid, Error>> HandleAsync(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var petId = PetId.CreateRandom();
        
        var specieId = SpecieId.Create(command.SpecieId).Value;

        var petType = new PetType(
            specieId, command.BreedId);

        var petName = ShortName.Create(command.Name).Value;

        var pet = new Pet(
            petId,
            petType,
            petName,
            null,
            null,
            null,
            null,
            null,
            null,
            null);
        
        volunteerResult.Value.AddPet(pet);
        
        var result = await _volunteersRepository.SaveAsync(volunteerResult.Value, cancellationToken);

        return result;
    }
}