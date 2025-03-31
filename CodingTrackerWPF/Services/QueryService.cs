using MySql.Data.MySqlClient;
using Dapper;
using System.Data;

namespace CodingTrackerWPF.Services;

internal class QueryService
{
    private string? _connectionString = App.ConfigurationJson["ConnectionStrings:DefaultConnection"];

    internal void ExecuteQuery(string query, object? parameters = null)
    {
        using (IDbConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(query, parameters);
        }
    }
}
