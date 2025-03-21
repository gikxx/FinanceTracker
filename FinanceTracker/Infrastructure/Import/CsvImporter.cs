using FinanceTracker.Domain.Models;
using System.Collections.Generic;
using System.IO;

namespace FinanceTracker.Infrastructure.Import
{
    public class CsvImporter : DataImporter
    {
        protected override string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        protected override List<BankAccount> ParseBankAccounts(string data)
        {
            var accounts = new List<BankAccount>();
            var lines = data.Split('\n');

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length != 4) continue;
                if (parts[0] != "Account") continue;

                if (int.TryParse(parts[1], out int id) && decimal.TryParse(parts[3], out decimal balance))
                {
                    accounts.Add(new BankAccount(id, parts[2], balance));
                }
            }
            return accounts;
        }
    }
}