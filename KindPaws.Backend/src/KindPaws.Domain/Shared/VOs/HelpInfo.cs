using KindPaws.Domain.Managements.PetManagement.Enums;

namespace KindPaws.Domain.Shared.VOs;

public class HelpInfo
{
    private readonly List<Requisite> _requisites;

    public HelpInfo(
        List<Requisite> requisites,
        HelpStatus status)
    {
        _requisites = requisites;
        Status = status;
    }

    public HelpStatus Status { get; private set; }
    public IReadOnlyList<Requisite> Requisites => _requisites;
}