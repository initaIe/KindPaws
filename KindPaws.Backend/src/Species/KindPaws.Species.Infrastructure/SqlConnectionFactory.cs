using System.Data;
using KindPaws.Core;
using KindPaws.Core.Abstractions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace KindPaws.Species.Infrastructure;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection Create() =>
        new NpgsqlConnection(_configuration.GetConnectionString(Constants.Database.Postgres));
}