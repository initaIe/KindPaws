using System.Data;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Framework.Options;
using Microsoft.Extensions.Options;
using Npgsql;

namespace KindPaws.Species.Infrastructure;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly PostgresOptions _postgresOptions;

    public SqlConnectionFactory(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public IDbConnection Create()
        => new NpgsqlConnection(_postgresOptions.ConnectionString);
}