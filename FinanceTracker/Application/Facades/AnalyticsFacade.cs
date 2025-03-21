using FinanceTracker.Application.Services;

public class AnalyticsFacade
{
    private readonly AnalyticsService _analyticsService;

    public AnalyticsFacade(AnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    public decimal CalculateNetIncome(DateTime startDate, DateTime endDate)
        => _analyticsService.CalculateNetIncome(startDate, endDate);
    
}