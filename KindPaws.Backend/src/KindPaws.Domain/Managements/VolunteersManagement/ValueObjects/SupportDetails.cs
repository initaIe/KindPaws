namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record SupportDetails
{
    private SupportDetails()
    {
    }

    public SupportDetails(
        SupportStatus status,
        List<Requisite> requisites)
    {
        Status = status;
        Requisites = requisites;
    }

    public SupportStatus Status { get; }
    public List<Requisite> Requisites { get; }
}