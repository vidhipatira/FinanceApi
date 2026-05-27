namespace FinanceApi.DTOs;

public class BudgetDto
{
    public int BudgetId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal AmountLimit { get; set; }
    public string MonthYear { get; set; } = string.Empty;
}