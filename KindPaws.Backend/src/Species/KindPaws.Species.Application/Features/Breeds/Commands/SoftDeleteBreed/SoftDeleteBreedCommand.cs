using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Breeds.Commands.SoftDeleteBreed;

public record SoftDeleteBreedCommand(
    Guid SpecieId,
    Guid BreedId)
    : ICommand;