using Npgsql;
using FinanceApi.DTOs;

namespace FinanceApi.Services;

public class IncomeService
{
    private readonly string _connectionString;

    public IncomeService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<IncomeDto>> GetIncome()
    {
        var incomeList = new List<IncomeDto>();

        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        var cmd = new NpgsqlCommand(@"
            SELECT 
                i.income_id,
                u.full_name,
                c.category_name,
                s.source_name,
                i.amount,
                i.income_date
            FROM income i
            JOIN users u ON i.user_id = u.user_id
            JOIN categories c ON i.category_id = c.category_id
            JOIN income_sources s ON i.source_id = s.source_id
        ", conn);

        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            incomeList.Add(new IncomeDto
            {
                IncomeId = reader.GetInt32(0),
                UserName = reader.GetString(1),
                CategoryName = reader.GetString(2),
                SourceName = reader.GetString(3),
                Amount = reader.GetDecimal(4),
                IncomeDate = reader.GetDateTime(5)
            });
        }

        return incomeList;
    }
}