using Npgsql;
using FinanceApi.DTOs;

namespace FinanceApi.Services;

public class UserService
{
    private readonly string _connectionString;

    public UserService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<UserDto>> GetUsers()
    {
        var users = new List<UserDto>();

        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        var cmd = new NpgsqlCommand(@"
            SELECT user_id, full_name, email
            FROM users
            ORDER BY user_id
        ", conn);

        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            users.Add(new UserDto
            {
                Id = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Email = reader.GetString(2)
            });
        }

        return users;
    }
}