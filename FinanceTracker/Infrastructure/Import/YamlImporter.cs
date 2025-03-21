using FinanceTracker.Domain.Models;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FinanceTracker.Infrastructure.Import
{
    public class YamlImporter : DataImporter
    {
        protected override string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        protected override List<BankAccount> ParseBankAccounts(string data)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();

            return deserializer.Deserialize<List<BankAccount>>(data);
        }
    }
}