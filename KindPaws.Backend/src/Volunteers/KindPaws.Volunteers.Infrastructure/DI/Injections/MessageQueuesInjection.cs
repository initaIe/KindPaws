using KindPaws.Core.Dtos;
using KindPaws.Core.Messaging;
using KindPaws.Volunteers.Infrastructure.MessageQueues;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Volunteers.Infrastructure.DI.Injections;

public static class MessageQueuesInjection
{
    public static IServiceCollection AddMessageQueues(this IServiceCollection services)
    {
        return services.AddSingleton<IMessageQueue<IEnumerable<DeleteFileData>>,
            FilesCleanerMessageQueue<IEnumerable<DeleteFileData>>>();
    }
}