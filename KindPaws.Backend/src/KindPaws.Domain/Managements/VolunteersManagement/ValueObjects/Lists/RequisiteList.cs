namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;

public record RequisiteList
{
    // ef core
    private RequisiteList()
    {
    }

    public RequisiteList(List<Requisite> requisites)
    {
        Requisites = requisites;
    }

    public IReadOnlyList<Requisite> Requisites { get; }
}