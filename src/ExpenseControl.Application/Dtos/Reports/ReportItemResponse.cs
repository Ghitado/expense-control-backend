namespace ExpenseControl.Application.Dtos.Reports;

public record ReportItemResponse(Guid Id, string Name, decimal Revenue, decimal Expense, decimal Balance);

