using System.Data;

namespace KindPaws.Core.Abstractions;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}