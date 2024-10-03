namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;

public record RequisiteList
{
    private readonly List<Requisite> _requisites;

    // ef core
    private RequisiteList()
    {
    }

    public RequisiteList(List<Requisite> requisites)
    {
        _requisites = requisites;
    }

    public IReadOnlyList<Requisite> Requisites => _requisites;
}