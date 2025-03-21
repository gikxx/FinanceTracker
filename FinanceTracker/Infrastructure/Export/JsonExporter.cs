using FinanceTracker.Application.DTOs;
using FinanceTracker.Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FinanceTracker.Infrastructure.Export
{
    public class JsonExporter : IExportVisitor
    {
        private readonly string _filePath;
        private readonly List<object> _data = new List<object>();

        public JsonExporter(string filePath)
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
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            File.WriteAllText(_filePath, JsonSerializer.Serialize(_data, options));
        }
    }
}