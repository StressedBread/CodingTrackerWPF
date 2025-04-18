using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using CodingTrackerWPF.Models;

namespace CodingTrackerWPF.Services;

public class QueryService
{
    private string? _connectionString = App.ConfigurationJson["ConnectionStrings:DefaultConnection"];

    public void ExecuteQuery(string query, object? parameters = null)
    {
        using (IDbConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(query, parameters);
        }
    }

    public async Task<List<CodingSession>> ReaderAsync(string query)
    {
        using (IDbConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var result = await connection.QueryAsync<CodingSession>(query).ConfigureAwait(false);
            return result.ToList();
        }
    }

    public async Task<T?> QueryAsync<T>(string query)
    {
        using (IDbConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QuerySingleOrDefaultAsync<T>(query).ConfigureAwait(false);
        }
    }
}
