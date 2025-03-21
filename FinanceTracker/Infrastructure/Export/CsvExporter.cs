using FinanceTracker.Application.DTOs;
using FinanceTracker.Domain.Models;
using System.Text;
using System.IO;

namespace FinanceTracker.Infrastructure.Export
{
    public class CsvExporter : IExportVisitor
    {
        private readonly string _filePath;
        private readonly StringBuilder _csvData = new StringBuilder();
        private bool _isHeaderWritten;

        public CsvExporter(string filePath)
        {
            _filePath = filePath;
        }

        public void Visit(BankAccount account)
        {
            var dto = new BankAccountDTO(account);

            if (!_isHeaderWritten)
            {
                _csvData.AppendLine("Type,Id,Name,Balance");
                _isHeaderWritten = true;
            }

            _csvData.AppendLine($"Account,{dto.Id},{EscapeCsv(dto.Name)},{dto.Balance}");
        }

        public void Visit(Category category)
        {
            var dto = new CategoryDTO(category);

            if (!_isHeaderWritten)
            {
                _csvData.AppendLine("Type,Id,CategoryType,Name");
                _isHeaderWritten = true;
            }

            _csvData.AppendLine($"Category,{dto.Id},{dto.Type},{EscapeCsv(dto.Name)}");
        }

        public void Visit(Operation operation)
        {
            var dto = new OperationDTO(operation);

            if (!_isHeaderWritten)
            {
                _csvData.AppendLine("Type,Id,OperationType,Amount,Date,CategoryId");
                _isHeaderWritten = true;
            }

            _csvData.AppendLine($"Operation,{dto.Id},{dto.Type},{dto.Amount},{dto.Date:yyyy-MM-dd},{dto.CategoryId}");
        }

        public void CompleteExport()
        {
            File.WriteAllText(_filePath, _csvData.ToString());
            _csvData.Clear();
            _isHeaderWritten = false;
        }

        private string EscapeCsv(string value)
        {
            return value?.Contains(",") == true ? $"\"{value}\"" : value;
        }
    }
}