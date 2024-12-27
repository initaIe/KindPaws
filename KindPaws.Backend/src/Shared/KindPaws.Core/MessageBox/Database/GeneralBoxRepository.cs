namespace KindPaws.Core.MessageBox.Database;

// TODO: REMOVE???

// public class GeneralBoxRepository : IGeneralBoxRepository
// {
//     private readonly IGeneralBoxDbContext _writeDbContext;
//
//     public GeneralBoxRepository(IGeneralBoxDbContext writeDbContext)
//     {
//         _writeDbContext = writeDbContext;
//     }
//
//     public async Task AddInBoxMessagesAsync<T>(
//         IEnumerable<T> messages,
//         CancellationToken cancellationToken = default)
//         where T : IEvent
//     {
//         var outboxMessages = messages.Select(InBoxMessage.CreateNew);
//         await _writeDbContext.OutBoxMessages.AddRangeAsync(outboxMessages, cancellationToken);
//     }
//     
//     public async Task AddOutBoxMessagesAsync<T>(
//         IEnumerable<T> messages,
//         CancellationToken cancellationToken = default)
//         where T : IEvent
//     {
//         var outboxMessages = messages.Select(BoxMessage.CreateNew);
//         await _writeDbContext.OutBoxMessages.AddRangeAsync(outboxMessages, cancellationToken);
//     }
// }