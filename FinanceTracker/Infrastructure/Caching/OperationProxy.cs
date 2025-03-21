using FinanceTracker.Domain.Enums;
using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;



namespace FinanceTracker.Infrastructure.Caching
{
    public class OperationProxy : IOperationService
    {
        private readonly IOperationService _operationService;
        private readonly Dictionary<int, Operation> _cache = new Dictionary<int, Operation>();
        private readonly string _filePath = "operations.json";

        public OperationProxy(IOperationService operationService)
        {
            _operationService = operationService;
            LoadDataFromFile(); // Загрузка данных из файла при инициализации
        }

        private void LoadDataFromFile()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                var operations = JsonSerializer.Deserialize<List<Operation>>(json);
                foreach (var operation in operations)
                {
                    CreateOperationWithId(operation.Id, operation.Type, operation.BankAccountId, operation.Amount, operation.Date, operation.CategoryId, operation.Description);
                }
                // Обновление id
            }
        }

        private void SaveDataToFile()
        {
            var json = JsonSerializer.Serialize(_cache.Values);
            File.WriteAllText(_filePath, json);
        }

        public Operation CreateOperation(
            OperationType type,
            int bankAccountId,
            decimal amount,
            DateTime date,
            int categoryId,
            string description = null
        )
        {
            var operation = _operationService.CreateOperation(type, bankAccountId, amount, date, categoryId, description);
            _cache[operation.Id] = operation;
            SaveDataToFile(); // Сохранение данных в файл
            return operation;
        }

        public Operation CreateOperationWithId(
                       int id,
                                  OperationType type,
                                             int bankAccountId,
                                                        decimal amount,
                                                                   DateTime date,
                                                                              int categoryId,
                                                                                         string description = null
                   )
        {
            var operation = _operationService.CreateOperationWithId(id, type, bankAccountId, amount, date, categoryId, description);
            _cache[operation.Id] = operation;
            SaveDataToFile(); // Сохранение данных в файл
            return operation;
        }

        public Operation GetOperationById(int operationId)
        {
            if (_cache.ContainsKey(operationId))
                return _cache[operationId];

            var operation = _operationService.GetOperationById(operationId);
            if (operation != null)
            {
                _cache[operationId] = operation;
            }
            return operation;
        }

        public List<Operation> GetOperationsByDateRange(DateTime startDate, DateTime endDate)
        {
            // Используем кэш для поиска операций
            return _cache.Values
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .ToList();
        }

        public void UpdateOperation(Operation operation)
        {
            _operationService.UpdateOperation(operation);
            _cache[operation.Id] = operation;
            SaveDataToFile(); // Сохранение данных в файл
        }

        public void DeleteOperation(int operationId)
        {
            _operationService.DeleteOperation(operationId);
            _cache.Remove(operationId);
            SaveDataToFile(); // Сохранение данных в файл
        }
        public List<Operation> GetAllOperations()
        {
            return _cache.Values.ToList();
        }
        private void SetNextId(int nextId)
        {
            if (_operationService is OperationService service)
            {
                service.SetNextOperationId(nextId);
            }
        }
    }
}