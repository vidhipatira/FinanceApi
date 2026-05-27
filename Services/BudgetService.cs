using Npgsql;
using FinanceApi.DTOs;

namespace FinanceApi.Services;

public class BudgetService
{
    private readonly string _connectionString;

    public BudgetService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<BudgetDto>> GetBudgets()
    {
        var budgets = new List<BudgetDto>();

        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        var cmd = new NpgsqlCommand(@"
            SELECT 
                b.budget_id,
                u.full_name,
                c.category_name,
                b.amount_limit,
                b.month_year
            FROM budgets b
            JOIN users u ON b.user_id = u.user_id
            JOIN categories c ON b.category_id = c.category_id
        ", conn);

        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            budgets.Add(new BudgetDto
            {
                BudgetId = reader.GetInt32(0),
                UserName = reader.GetString(1),
                CategoryName = reader.GetString(2),
                AmountLimit = reader.GetDecimal(3),
                MonthYear = reader.GetString(4)
            });
        }

        return budgets;
    }
}