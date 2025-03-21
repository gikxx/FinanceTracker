using FinanceTracker.Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FinanceTracker.Infrastructure.Import
{
    public class JsonImporter : DataImporter
    {
        protected override string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        protected override List<BankAccount> ParseBankAccounts(string data)
        {
            var accounts = JsonSerializer.Deserialize<List<BankAccount>>(data);
            foreach (var account in accounts)
            {
                if (account.Balance < 0)
                    throw new InvalidDataException("Баланс не может быть отрицательным.");
            }
            return accounts;
        }
    }
}