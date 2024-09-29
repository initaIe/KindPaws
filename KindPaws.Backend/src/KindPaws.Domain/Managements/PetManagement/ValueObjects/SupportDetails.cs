namespace KindPaws.Domain.Managements.PetManagement.ValueObjects;

public record SupportDetails
{
    public SupportDetails()
    {
    }

    public SupportDetails(
        SupportStatus status,
        List<Requisite> requisites)
    {
        Status = status;
        Requisites = requisites;
    }

    public SupportStatus Status { get; private set; }
    public List<Requisite> Requisites { get; private set; }
}