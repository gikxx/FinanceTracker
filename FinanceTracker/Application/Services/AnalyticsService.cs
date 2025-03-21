using FinanceTracker.Domain.Enums;
using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceTracker.Application.Services
{
    public class AnalyticsService
    {
        private readonly IOperationService _operationService;

        public AnalyticsService(IOperationService operationService)
        {
            _operationService = operationService;
        }

        public decimal CalculateNetIncome(DateTime startDate, DateTime endDate)
        {
            var operations = _operationService.GetOperationsByDateRange(startDate, endDate);

            decimal totalIncome = operations
                .Where(o => o.Type == OperationType.Income)
                .Sum(o => o.Amount);

            decimal totalExpense = operations
                .Where(o => o.Type == OperationType.Expense)
                .Sum(o => o.Amount);

            return totalIncome - totalExpense;
        }
    }
}