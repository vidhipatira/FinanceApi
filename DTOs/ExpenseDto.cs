namespace FinanceApi.DTOs;

public class ExpenseDto
{
    public int ExpenseId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime ExpenseDate { get; set; }
}