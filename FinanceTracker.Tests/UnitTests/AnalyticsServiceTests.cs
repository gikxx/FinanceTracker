using Xunit;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.Services;
using FinanceTracker.Application.Services;
using System;
using System.Collections.Generic;
using FinanceTracker.Domain.Interfaces;
using Moq;

namespace FinanceTracker.Tests.UnitTests
{
    public class AnalyticsServiceTests
    {
        [Fact]
        public void CalculateNetIncome_ValidData_ReturnsCorrectNetIncome()
        {
            // Arrange
            var mockBankAccountService = new Mock<IBankAccountService>(); // Создаем mock для IBankAccountService
            var operationService = new OperationService(mockBankAccountService.Object);
            var analyticsService = new AnalyticsService(operationService);

            var accountId = 1;
            var categoryId = 1;

            operationService.CreateOperation(
                OperationType.Income,
                accountId,
                1000,
                DateTime.Now,
                categoryId
            );

            operationService.CreateOperation(
                OperationType.Expense,
                accountId,
                300,
                DateTime.Now,
                categoryId
            );

            // Act
            var netIncome = analyticsService.CalculateNetIncome(DateTime.MinValue, DateTime.MaxValue);

            // Assert
            Assert.Equal(700, netIncome);
        }
    }
}