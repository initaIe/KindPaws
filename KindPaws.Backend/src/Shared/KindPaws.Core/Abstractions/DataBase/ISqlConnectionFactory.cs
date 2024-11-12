using System.Data;

namespace KindPaws.Core.Abstractions.DataBase;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}