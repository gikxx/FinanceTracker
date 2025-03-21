using FinanceTracker.Domain.Models;

namespace FinanceTracker.Domain.Interfaces
{
    public interface IBankAccountService
    {
        BankAccount CreateAccount(string name, decimal initialBalance);
        BankAccount CreateAccountWithId(int id, string name, decimal initialBalance);
        void UpdateAccount(BankAccount account);
        void DeleteAccount(int accountId);
        BankAccount GetAccountById(int accountId);
        List<BankAccount> GetAllAccounts();
        void Deposit(int accountId, decimal amount);
        void Withdraw(int accountId, decimal amount);
    }
}
