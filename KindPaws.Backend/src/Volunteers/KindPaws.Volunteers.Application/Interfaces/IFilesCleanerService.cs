namespace KindPaws.Volunteers.Application.Interfaces;

public interface IFilesCleanerService
{
    Task ProcessAsync(CancellationToken cancellationToken = default);
}