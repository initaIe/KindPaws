namespace KindPaws.Domain.Managements.BreedManagement.ValuseObjects.Lists;

public record BreedColorList
{
    private readonly List<BreedColor> _breedColors;

    public BreedColorList()
    {
    }

    public BreedColorList(List<BreedColor> breedColors)
    {
        _breedColors = breedColors;
    }

    public IReadOnlyList<BreedColor> BreedColors => _breedColors;
}