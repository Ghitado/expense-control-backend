namespace ExpenseControl.Application.Dtos.Reports;

public record BalanceReportResponse(
	List<ReportItemResponse> Items,
	decimal TotalRevenue,
	decimal TotalExpense,
	decimal TotalBalance
);

