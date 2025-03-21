using FinanceTracker.Application.DTOs;
using FinanceTracker.Domain.Models;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FinanceTracker.Infrastructure.Export
{
    public class YamlExporter : IExportVisitor
    {
        private readonly string _filePath;
        private readonly List<object> _data = new List<object>();

        public YamlExporter(string filePath)
        {
            _filePath = filePath;
        }

        public void Visit(BankAccount account)
        {
            _data.Add(new BankAccountDTO(account));
        }

        public void Visit(Category category)
        {
            _data.Add(new CategoryDTO(category));
        }

        public void Visit(Operation operation)
        {
            _data.Add(new OperationDTO(operation));
        }

        public void CompleteExport()
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();

            File.WriteAllText(_filePath, serializer.Serialize(_data));
        }
    }
}