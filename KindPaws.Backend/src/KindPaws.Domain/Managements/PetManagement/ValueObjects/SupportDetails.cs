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

    public SupportStatus Status { get; }
    public List<Requisite> Requisites { get; }
}