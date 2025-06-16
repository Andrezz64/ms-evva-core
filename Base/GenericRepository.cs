using Dapper;
using ms_evva_core.Repos.Interfaces;
using ms_evva_core.Utils;
using System.Data;
using System.Reflection;

namespace ms_evva_core.Base;

public class GenericRepository<T> : IRepository<T> where T : class
{
    protected string TableName { get; }
    protected readonly ConnectionProvider _connectionProvider = new();

    public GenericRepository(string tableName)
    {
        TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));

    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        using var connection = await _connectionProvider.CreateConnectionAsync();
        return await connection.QueryFirstOrDefaultAsync<T>(
            $"SELECT * FROM {TableName} WHERE id = @Id", new { Id = id });
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        using var connection = await _connectionProvider.CreateConnectionAsync();
        return await connection.QueryAsync<T>($"SELECT * FROM {TableName}");
    }

    public virtual async Task<int> AddAsync(T entity)
    {
        using var connection = await _connectionProvider.CreateConnectionAsync();
        var properties = typeof(T).GetProperties()
            .Where(p => p.Name.ToLower() != "id")
            .Select(p => p.Name.ToLower());

        var columns = string.Join(", ", properties);
        var parameters = string.Join(", ", properties.Select(p => $"@{p}"));

        var sql = $"INSERT INTO {TableName} ({columns}) VALUES ({parameters}) RETURNING id";
        return await connection.ExecuteScalarAsync<int>(sql, entity);
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        using var connection = await _connectionProvider.CreateConnectionAsync();
        var properties = typeof(T).GetProperties()
            .Where(p => p.Name.ToLower() != "id")
            .Select(p => $"{p.Name.ToLower()} = @{p.Name}");

        var sql = $"UPDATE {TableName} SET {string.Join(", ", properties)} WHERE id = @Id";
        return await connection.ExecuteAsync(sql, entity) > 0;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        using var connection = await _connectionProvider.CreateConnectionAsync();
        var sql = $"DELETE FROM {TableName} WHERE id = @Id";
        return await connection.ExecuteAsync(sql, new { Id = id }) > 0;
    }
}