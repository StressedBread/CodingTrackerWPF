using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.ViewModels;
using CodingTrackerWPF.Views;

namespace CodingTrackerWPF.Services;

public class QueryService
{
    private readonly string? _connectionString = App.ConfigurationJson?["ConnectionStrings:DefaultConnection"];
    
    public void ExecuteQuery(string query, object? parameters = null)
    {
        try
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();
            connection.Execute(query, parameters); 
        }
        catch (Exception ex)
        {
            HandleSqlException(ex);
        }
    }

    public async Task<List<CodingSession>?> ReaderAsync(string query)
    {
        try
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();
            var result = await connection.QueryAsync<CodingSession>(query).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            HandleSqlException(ex);
            return null;
        }
    }

    public async Task<T?> QueryAsync<T>(string query, object? parameters = null)
    {
        try
        { 
            using MySqlConnection connection = new(_connectionString);
            connection.Open();
            return await connection.QuerySingleOrDefaultAsync<T>(query, parameters).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            HandleSqlException(ex);
            return default;
        }
    }

    public T? Query<T>(string query, object? parameters = null)
    {
        try
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();
            return connection.QuerySingleOrDefault<T>(query, parameters);
        }
        catch (Exception ex)
        {
            HandleSqlException(ex);
            return default;
        }
    }

    public void HandleSqlException(Exception ex)
    {
        _ = new MessageDialogView
        {
            DataContext = new MessageDialogViewModel("Error", "Couldn't connect to the database!")
        };
    }
}
