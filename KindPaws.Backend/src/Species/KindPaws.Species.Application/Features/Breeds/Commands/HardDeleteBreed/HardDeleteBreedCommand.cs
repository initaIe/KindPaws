using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Breeds.Commands.HardDeleteBreed;

public record HardDeleteBreedCommand(
    Guid SpecieId,
    Guid BreedId)
    : ICommand;