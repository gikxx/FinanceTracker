using FinanceTracker.Domain.Enums;
using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Domain.Models;

public class OperationFacade
{
    private readonly IOperationService _operationService;

    public OperationFacade(IOperationService operationService)
    {
        _operationService = operationService;
    }

    public List<Operation> GetAllOperations()
        => _operationService.GetAllOperations(); // Добавленный метод

    public Operation CreateOperation(
    OperationType type,
    int bankAccountId,
    decimal amount,
    DateTime date,
    int categoryId,
    string description = null)
    {
        return _operationService.CreateOperation(type, bankAccountId, amount, date, categoryId, description);
    }
    public void UpdateOperation(Operation operation)
        => _operationService.UpdateOperation(operation);

    public void DeleteOperation(int operationId)
        => _operationService.DeleteOperation(operationId);
}