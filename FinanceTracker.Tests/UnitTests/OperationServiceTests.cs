using Xunit;
using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.Services;
using FinanceTracker.Domain.Enums;
using System;
using System.Linq;

namespace FinanceTracker.Tests.UnitTests
{
    public class OperationServiceTests
    {
        [Fact]
        public void CreateOperation_Income_BalanceIncreased()
        {
            // Arrange
            var bankAccountService = new BankAccountService();
            var operationService = new OperationService(bankAccountService);
            var account = bankAccountService.CreateAccount("Тестовый счет", 1000);
            var category = new Category(1, CategoryType.Income, "Зарплата");

            // Act
            var operation = operationService.CreateOperation(
                OperationType.Income,
                account.Id,
                500,
                DateTime.Now,
                category.Id
            );

            // Assert
            var updatedAccount = bankAccountService.GetAccountById(account.Id);
            Assert.Equal(1500, updatedAccount.Balance);
        }

        [Fact]
        public void CreateOperation_Expense_BalanceDecreased()
        {
            // Arrange
            var bankAccountService = new BankAccountService();
            var operationService = new OperationService(bankAccountService);
            var account = bankAccountService.CreateAccount("Тестовый счет", 1000);
            var category = new Category(1, CategoryType.Expense, "Кафе");

            // Act
            var operation = operationService.CreateOperation(
                OperationType.Expense,
                account.Id,
                300,
                DateTime.Now,
                category.Id
            );

            // Assert
            var updatedAccount = bankAccountService.GetAccountById(account.Id);
            Assert.Equal(700, updatedAccount.Balance);
        }

        [Fact]
        public void DeleteOperation_ValidId_OperationDeleted()
        {
            // Arrange
            var bankAccountService = new BankAccountService();
            var operationService = new OperationService(bankAccountService);
            var account = bankAccountService.CreateAccount("Тестовый счет", 1000);
            var category = new Category(1, CategoryType.Income, "Зарплата");

            var operation = operationService.CreateOperation(
                OperationType.Income,
                account.Id,
                500,
                DateTime.Now,
                category.Id
            );

            // Act
            operationService.DeleteOperation(operation.Id);

            // Assert
            Assert.Throws<KeyNotFoundException>(() => operationService.GetOperationById(operation.Id));
        }
    }
}