namespace KindPaws.Volunteers.Application.Abstractions;

public interface IFilesCleanerService
{
    Task ProcessAsync(CancellationToken cancellationToken = default);
}