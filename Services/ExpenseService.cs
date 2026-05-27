using Npgsql;
using FinanceApi.DTOs;

namespace FinanceApi.Services;

public class ExpenseService
{
    private readonly string _connectionString;

    public ExpenseService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<ExpenseDto>> GetExpenses()
    {
        var list = new List<ExpenseDto>();

        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        var cmd = new NpgsqlCommand(@"
            SELECT e.expense_id, u.full_name, c.category_name,
                   e.amount, e.description, e.expense_date
            FROM expenses e
            JOIN users u ON e.user_id = u.user_id
            JOIN categories c ON e.category_id = c.category_id
        ", conn);

        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(new ExpenseDto
            {
                ExpenseId = reader.GetInt32(0),
                UserName = reader.GetString(1),
                CategoryName = reader.GetString(2),
                Amount = reader.GetDecimal(3),
                Description = reader.IsDBNull(4) ? null : reader.GetString(4),
                ExpenseDate = reader.GetDateTime(5)
            });
        }

        return list;
    }
}