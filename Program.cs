using FinanceApi.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. DYNAMIC CONNECTION STRING (Crucial for AWS deployment later)
// This checks if AWS injected a database connection string. If not, it falls back to your local DB.
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") 
    ?? "Host=finance-db-prod.cxqi468ymmhh.ca-central-1.rds.amazonaws.com;Port=5432;Database=Spendwise;Username=postgres;Password=Password123;";


// SERVICES
builder.Services.AddScoped(_ => new UserService(connectionString));
builder.Services.AddScoped(_ => new ExpenseService(connectionString));
builder.Services.AddScoped(_ => new IncomeService(connectionString));
builder.Services.AddScoped(_ => new BudgetService(connectionString));

// Register the health check service
builder.Services.AddHealthChecks();

// BUILD THE APP
var app = builder.Build(); // 'app' is officially declared here!

// MAP THE HEALTH CHECK (Now safe to use 'app')
app.MapHealthChecks("/api/health");

// ROOT
app.MapGet("/", () => "Finance API running");

// USERS
app.MapGet("/users", async (UserService service) =>
{
    return await service.GetUsers();
});

// EXPENSES
app.MapGet("/expenses", async (ExpenseService service) =>
{
    return await service.GetExpenses();
});

// INCOME
app.MapGet("/income", async (IncomeService service) =>
{
    return await service.GetIncome();
});

// BUDGETS
app.MapGet("/budgets", async (BudgetService service) =>
{
    return await service.GetBudgets();
});

app.Run();