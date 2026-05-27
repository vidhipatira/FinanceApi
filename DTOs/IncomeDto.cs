namespace FinanceApi.DTOs;

public class IncomeDto
{
    public int IncomeId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string SourceName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime IncomeDate { get; set; }
}