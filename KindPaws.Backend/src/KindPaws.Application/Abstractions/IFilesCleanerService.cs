namespace KindPaws.Application.Abstractions;

public interface IFilesCleanerService
{
    Task ProcessAsync(CancellationToken cancellationToken);
}