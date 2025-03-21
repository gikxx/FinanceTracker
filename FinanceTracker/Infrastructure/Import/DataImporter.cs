using FinanceTracker.Domain.Models;
using System.Collections.Generic;

namespace FinanceTracker.Infrastructure.Import
{
    public abstract class DataImporter
    {
        public List<BankAccount> ImportBankAccounts(string filePath)
        {
            var data = ReadFile(filePath);
            return ParseBankAccounts(data);
        }

        protected abstract string ReadFile(string filePath);
        protected abstract List<BankAccount> ParseBankAccounts(string data);
    }
}