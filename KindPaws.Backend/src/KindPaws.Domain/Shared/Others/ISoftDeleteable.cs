namespace KindPaws.Domain.Shared.Others;

public interface ISoftDeleteable
{
    void Delete();
    void Restore();
}