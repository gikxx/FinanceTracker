using Xunit;
using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.Services;
using FinanceTracker.Domain.Enums;
using System;

namespace FinanceTracker.Tests.UnitTests
{
    public class BankAccountServiceTests
    {
        [Fact]
        public void CreateAccount_ValidData_AccountCreated()
        {
            // Arrange
            var service = new BankAccountService();
            string name = "Основной счет";
            decimal initialBalance = 1000;

            // Act
            var account = service.CreateAccount(name, initialBalance);

            // Assert
            Assert.Equal(name, account.Name);
            Assert.Equal(initialBalance, account.Balance);
        }

        [Fact]
        public void Deposit_ValidAmount_BalanceIncreased()
        {
            // Arrange
            var service = new BankAccountService();
            var account = service.CreateAccount("Тестовый счет", 500);

            // Act
            service.Deposit(account.Id, 300);

            // Assert
            var updatedAccount = service.GetAccountById(account.Id);
            Assert.Equal(800, updatedAccount.Balance);
        }

        [Fact]
        public void Withdraw_InsufficientBalance_ThrowsException()
        {
            // Arrange
            var service = new BankAccountService();
            var account = service.CreateAccount("Тестовый счет", 500);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.Withdraw(account.Id, 600));
        }

        [Fact]
        public void DeleteAccount_ValidId_AccountDeleted()
        {
            // Arrange
            var service = new BankAccountService();
            var account = service.CreateAccount("Тестовый счет", 1000);

            // Act
            service.DeleteAccount(account.Id);

            // Assert
            Assert.Throws<KeyNotFoundException>(() => service.GetAccountById(account.Id));
        }
    }
}