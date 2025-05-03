using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using CodingTrackerWPF.Models;

namespace CodingTrackerWPF.Services;

public class QueryService
{
    private readonly string? _connectionString = App.ConfigurationJson?["ConnectionStrings:DefaultConnection"];

    public void ExecuteQuery(string query, object? parameters = null)
    {
        using MySqlConnection connection = new(_connectionString);
        connection.Open();
        connection.Execute(query, parameters);
    }

    public async Task<List<CodingSession>> ReaderAsync(string query)
    {
        using MySqlConnection connection = new(_connectionString);
        connection.Open();
        var result = await connection.QueryAsync<CodingSession>(query).ConfigureAwait(false);
        return result.ToList();
    }

    public async Task<T?> QueryAsync<T>(string query, object? parameters = null)
    {
        using MySqlConnection connection = new(_connectionString);
        connection.Open();
        return await connection.QuerySingleOrDefaultAsync<T>(query, parameters).ConfigureAwait(false);
    }

    public T? Query<T>(string query, object? parameters = null)
    {
        using MySqlConnection connection = new(_connectionString);
        connection.Open();
        return connection.QuerySingleOrDefault<T>(query, parameters);
    }
}
