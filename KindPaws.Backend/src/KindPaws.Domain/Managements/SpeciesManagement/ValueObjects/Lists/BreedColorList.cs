namespace KindPaws.Domain.Managements.SpeciesManagement.ValueObjects.Lists;

public record BreedColorList
{
    private readonly List<BreedColor> _breedColors;

    private BreedColorList()
    {
    }

    public BreedColorList(List<BreedColor> breedColors)
    {
        _breedColors = breedColors;
    }

    public IReadOnlyList<BreedColor> BreedColors => _breedColors;
}