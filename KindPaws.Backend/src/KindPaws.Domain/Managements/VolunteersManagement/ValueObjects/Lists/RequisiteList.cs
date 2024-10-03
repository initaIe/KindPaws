namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;

public record RequisiteList
{
    // ef core
    private RequisiteList()
    {
    }

    public RequisiteList(IEnumerable<Requisite> requisites)
    {
        Requisites = requisites.ToList();
    }

    public IReadOnlyList<Requisite> Requisites { get; }
}