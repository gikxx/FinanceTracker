using FinanceTracker.Domain.Enums;
using FinanceTracker.Domain.Models;

namespace FinanceTracker.Domain.Interfaces
{
    public interface IOperationService
    {
        Operation CreateOperation(
            OperationType type,
            int bankAccountId,
            decimal amount,
            DateTime date,
            int categoryId,
            string description = null
        );
        Operation CreateOperationWithId(
                       int id,
                                  OperationType type,
                                             int bankAccountId,
                                                        decimal amount,
                                                                   DateTime date,
                                                                              int categoryId,
                                                                                         string description = null
                   );
        void UpdateOperation(Operation operation);
        void DeleteOperation(int operationId);
        Operation GetOperationById(int operationId);
        List<Operation> GetOperationsByDateRange(DateTime startDate, DateTime endDate);
        List<Operation> GetAllOperations();
    }
}