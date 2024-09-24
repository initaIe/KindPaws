using KindPaws.Domain.Enums;

namespace KindPaws.Domain;

public class HelpInfo
{
    private readonly List<HelpDetail> _details;

    public HelpInfo(
        List<HelpDetail> details,
        HelpStatus status)
    {
        _details = details;
        Status = status;
    }

    public HelpStatus Status { get; private set; }
    public IReadOnlyList<HelpDetail> Details => _details;
}