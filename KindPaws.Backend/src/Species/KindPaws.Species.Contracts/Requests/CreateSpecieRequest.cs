namespace KindPaws.Species.Contracts.Requests;

public record CreateSpecieRequest(
    string Name,
    string Description);