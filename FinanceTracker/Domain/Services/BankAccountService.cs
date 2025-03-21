using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Domain.Models;
using System.Collections.Generic;

namespace FinanceTracker.Domain.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly List<BankAccount> _accounts = new List<BankAccount>();
        private int _nextAccountId = 1;

        public BankAccount CreateAccount(string name, decimal initialBalance)
        {
            var account = new BankAccount(_nextAccountId++, name, initialBalance);
            _accounts.Add(account);
            return account;
        }
        public BankAccount CreateAccountWithId(int id, string name, decimal initialBalance)
        {
            var account = new BankAccount(id, name, initialBalance);
            _accounts.Add(account);
            if (id >= _nextAccountId)
                _nextAccountId = id + 1;
            return account;
        }

        public void UpdateAccount(BankAccount account)
        {
            var existingAccount = _accounts.Find(a => a.Id == account.Id);
            if (existingAccount == null)
                throw new KeyNotFoundException("Счет не найден.");

            existingAccount.Name = account.Name;
        }

        public void DeleteAccount(int accountId)
        {
            var account = _accounts.Find(a => a.Id == accountId);
            if (account == null)
                throw new KeyNotFoundException("Счет не найден.");

            _accounts.Remove(account);
        }

        public BankAccount GetAccountById(int accountId)
        {
            return _accounts.Find(a => a.Id == accountId)
                ?? throw new KeyNotFoundException("Счет не найден.");
        }

        public void Deposit(int accountId, decimal amount)
        {
            var account = GetAccountById(accountId);
            account.Deposit(amount);
        }

        public void Withdraw(int accountId, decimal amount)
        {
            var account = GetAccountById(accountId);
            account.Withdraw(amount);
        }
        public List<BankAccount> GetAllAccounts()
        {
            return _accounts.ToList(); // Возвращаем копию списка
        }
        public void SetNextAccountId(int nextId)
        {
            _nextAccountId = nextId;
        }

        public bool AccountExistsById(int id)
        {
            return _accounts.Any(a => a.Id == id);
        }

    }
}