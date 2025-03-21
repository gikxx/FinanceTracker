using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.Services;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FinanceTracker.Infrastructure.Caching
{
    public class BankAccountProxy : IBankAccountService
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly Dictionary<int, BankAccount> _cache = new Dictionary<int, BankAccount>();
        private readonly string _filePath = "accounts.json";

        public BankAccountProxy(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
            LoadDataFromFile(); // Загрузка данных из файла при инициализации
        }

        private void LoadDataFromFile()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                var accounts = JsonSerializer.Deserialize<List<BankAccount>>(json);
                foreach (var account in accounts)
                {
                    CreateAccountWithId(account.Id, account.Name, account.Balance);
                }
            }
        }

        private void SaveDataToFile()
        {
            var json = JsonSerializer.Serialize(_cache.Values);
            File.WriteAllText(_filePath, json);
        }

        public BankAccount CreateAccount(string name, decimal initialBalance)
        {

            var account = _bankAccountService.CreateAccount(name, initialBalance);
            _cache[account.Id] = account;
            SaveDataToFile(); // Сохранение данных в файл
            return account;
        }
        public BankAccount CreateAccountWithId(int id, string name, decimal initialBalance)
        {
            if (_cache.ContainsKey(id))
            {
                throw new InvalidOperationException($"Счет с ID {id} уже существует.");
            }
            var account = _bankAccountService.CreateAccountWithId(id, name, initialBalance);
            _cache[account.Id] = account;
            SaveDataToFile(); // Сохранение данных в файл
            return account;
        }
        public BankAccount GetAccountById(int accountId)
        {
            if (_cache.ContainsKey(accountId))
                return _cache[accountId];

            var account = _bankAccountService.GetAccountById(accountId);
            if (account != null)
            {
                _cache[accountId] = account;
            }
            return account;
        }

        public void UpdateAccount(BankAccount account)
        {
            _bankAccountService.UpdateAccount(account);
            _cache[account.Id] = account;
            SaveDataToFile(); // Сохранение данных в файл
        }

        public void DeleteAccount(int accountId)
        {
            _bankAccountService.DeleteAccount(accountId);
            _cache.Remove(accountId);
            SaveDataToFile(); // Сохранение данных в файл
        }

        public void Deposit(int accountId, decimal amount)
        {
            var account = GetAccountById(accountId);
            account.Deposit(amount); // Используем метод модели
            SaveDataToFile(); // Сохранение данных в файл
        }

        public void Withdraw(int accountId, decimal amount)
        {
            var account = GetAccountById(accountId);
            account.Withdraw(amount); // Используем метод модели
            SaveDataToFile(); // Сохранение данных в файл
        }
        public List<BankAccount> GetAllAccounts()
        {
            // Возвращаем копию кэша для избежания изменений извне
            return _cache.Values.ToList();

        }
        private void SetNextAccountId(int nextId)
        {
            if (_bankAccountService is BankAccountService service)
            {
                service.SetNextAccountId(nextId);
            }
        }
    }
}   