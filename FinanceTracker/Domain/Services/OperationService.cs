using FinanceTracker.Domain.Enums;
using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Domain.Models;
using System.Collections.Generic;

namespace FinanceTracker.Domain.Services
{
    public class OperationService : IOperationService
    {
        private readonly List<Operation> _operations = new List<Operation>();
        private int _nextOperationId = 1;
        private readonly IBankAccountService _bankAccountService;

        public OperationService(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
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
            var operation = new Operation(
                _nextOperationId++,
                type,
                bankAccountId,
                amount,
                date,
                categoryId,
                description
            );

            // Обновляем баланс счета
            if (type == OperationType.Income)
                _bankAccountService.Deposit(bankAccountId, amount);
            else
                _bankAccountService.Withdraw(bankAccountId, amount);

            _operations.Add(operation);
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
            var operation = new Operation(
                               id,
                                              type,
                                                             bankAccountId,
                                                                            amount,
                                                                                           date,
                                                                                                          categoryId,
                                                                                                                         description
                                                                                                                                    );

            // Обновляем баланс счета
            if (type == OperationType.Income)
                _bankAccountService.Deposit(bankAccountId, amount);
            else
                _bankAccountService.Withdraw(bankAccountId, amount);

            _operations.Add(operation);
            if (id >= _nextOperationId)
                _nextOperationId = id + 1;
            return operation;
        }

        public void UpdateOperation(Operation operation)
        {
            var existingOperation = _operations.Find(o => o.Id == operation.Id);
            if (existingOperation == null)
                throw new KeyNotFoundException("Операция не найдена.");

            existingOperation.Type = operation.Type;
            existingOperation.Date = operation.Date;
            existingOperation.Description = operation.Description;
            existingOperation.CategoryId = operation.CategoryId;
        }

        public void DeleteOperation(int operationId)
        {
            var operation = _operations.Find(o => o.Id == operationId);
            if (operation == null)
                throw new KeyNotFoundException("Операция не найдена.");

            _operations.Remove(operation);
        }

        public Operation GetOperationById(int operationId)
        {
            return _operations.Find(o => o.Id == operationId)
                ?? throw new KeyNotFoundException("Операция не найдена.");
        }

        public List<Operation> GetOperationsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _operations
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .ToList();
        }
        public List<Operation> GetAllOperations()
        {
            return _operations.ToList(); // Возвращаем копию списка
        }
        public void SetNextOperationId(int nextId)
        {
            _nextOperationId = nextId;
        }
    }
}